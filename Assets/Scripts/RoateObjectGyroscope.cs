using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoateObjectGyroscope : MonoBehaviour
{
    Gyroscope gyro;
    bool gyroEnabled; //is gyroscope is avaliable or not
    Quaternion rotationAdjustment; //adjust the meaasurement for landscape mode (gyro measures from portrait mode)
    [SerializeField] TextMeshProUGUI rotationValueGyro;
    [SerializeField] TextMeshProUGUI rotationValueObject;
    [SerializeField] TextMeshProUGUI rotationValueResult;
    [SerializeField] TextMeshProUGUI rotationStatus;
    bool isReady;
    bool starterTimerOver;

    [SerializeField] float timeBeforeStart = 2f;
    [SerializeField] float startXThresholdValue;
    [SerializeField] float startZThresholdValue;
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroEnabled = true;
            gyro = Input.gyro;
            gyro.enabled = true;
        }

        else
        {
            rotationStatus.text = "Gyroscope not supported";
        }

        if (gyroEnabled)
        {
            rotationStatus.text = "Starting...";
            Invoke("StarterTimer", timeBeforeStart);
        }
    }

    void StarterTimer()
    {
        starterTimerOver = true;
        rotationStatus.text = "Rotate phone to good orientation";
    }

    void Update()
    {
        if (!gyroEnabled)
        {
            return;
        }

        Vector3 result = CalculateRotation();

        if (!isReady)
        {
            if (starterTimerOver)
            {
                if (Mathf.Abs(result.x) <= startXThresholdValue
            && Mathf.Abs(result.z) <= startZThresholdValue)
                {
                    isReady = true;
                    rotationStatus.text = "Start rotating";
                }
            }
        }

        if (isReady)
        {
            transform.localRotation = Quaternion.Euler(result);
            rotationValueResult.text = "Rotation object: " + result;
        }
    }

    //where we take the gyro orientation values and turn them into values at fit the controls
    Vector3 CalculateRotation()
    {
        Quaternion rotationAdjustment = Quaternion.Euler(0, 0, 90) * Quaternion.Inverse(gyro.attitude); //find the rotation diff btw the orginal orientation and current orientation
        Vector3 rotationAdjustmentAdjusted = (rotationAdjustment).eulerAngles;


        //adjust the x axis diff values to suit the game's control
        if (rotationAdjustmentAdjusted.x > 180)
        {
            rotationAdjustmentAdjusted.x = 360f - rotationAdjustmentAdjusted.x;
            rotationAdjustmentAdjusted.x = rotationAdjustmentAdjusted.x * -1;
        }

        else if (rotationAdjustmentAdjusted.x < 180)
        {

        }

        //adjust the y axis diff values to suit the game's control
        if (rotationAdjustmentAdjusted.y > 180)
        {
            rotationAdjustmentAdjusted.y = 360f - rotationAdjustmentAdjusted.y;
        }

        else if (rotationAdjustmentAdjusted.y < 180)
        {
            rotationAdjustmentAdjusted.y = rotationAdjustmentAdjusted.y * -1;
        }

        rotationAdjustmentAdjusted.y *= -1;

        //switch y and z
        Vector3 gyroRotationEulerResult = rotationAdjustmentAdjusted;
        float theZValue = gyroRotationEulerResult.z;
        gyroRotationEulerResult.z = gyroRotationEulerResult.y;
        gyroRotationEulerResult.y = 0;

        rotationValueGyro.text = "Rotation gyro: " + gyro.attitude.eulerAngles;
        rotationValueObject.text = "Rotation result: " + gyroRotationEulerResult;
        return gyroRotationEulerResult;
    }
}
