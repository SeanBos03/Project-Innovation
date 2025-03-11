using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] bool byPassStopper = true;

    [SerializeField] float timeBeforeStart = 2f;
    [SerializeField] float startXThresholdValue;
    [SerializeField] float startZThresholdValue;
    [SerializeField] float rotateSpeed = 1.5f;
    [SerializeField] float xMaxValue;
    [SerializeField] float zMaxValue;

    [SerializeField] bool invertX;
    [SerializeField] bool invertZ;
    [SerializeField] bool switchValues;
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
                    
                    if (gameObject.tag == "MainArea")
                    {
                        GameData.rotationReady = true;
                    }
                }
            }
        }

        if (GameData.shouldRoate || byPassStopper)
        {
            if (isReady)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(result), Time.deltaTime * rotateSpeed);
                rotationValueResult.text = "Rotation object: " + result;
            }
        }
    }

    //where we take the gyro orientation values and turn them into values at fit the controls
    Vector3 CalculateRotation()
    {
        Quaternion rotationAdjustment = Quaternion.Euler(0, 0, 90) * Quaternion.Inverse(gyro.attitude); //find the rotation diff btw the orginal orientation and current orientation
        rotationValueResult.text = "RotationAdjustment: " + rotationAdjustment.eulerAngles;
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
        gyroRotationEulerResult.z = gyroRotationEulerResult.y;
        gyroRotationEulerResult.y = 0;

        //adjust values so the they dont exceed the limit
        bool xIsNegative = false;
        bool zIsNegative = false;

        if (gyroRotationEulerResult.x < 0)
        {
            gyroRotationEulerResult.x *= -1;
            xIsNegative = true;
        }

        if (gyroRotationEulerResult.z < 0)
        {
            gyroRotationEulerResult.z *= -1;
            zIsNegative = true;
        }

        if (gyroRotationEulerResult.x > xMaxValue)
        {
            gyroRotationEulerResult.x = xMaxValue;
        }

        if (gyroRotationEulerResult.z > zMaxValue)
        {
            gyroRotationEulerResult.z = zMaxValue;
        }

        if (xIsNegative)
        {
            gyroRotationEulerResult.x *= -1;
        }

        if (zIsNegative)
        {
            gyroRotationEulerResult.z *= -1;
        }

        //the result stage
        if (invertX)
        {
            gyroRotationEulerResult.x *= -1;
        }

        if (invertZ)
        {
            gyroRotationEulerResult.z *= -1;
        }

        if (switchValues)
        {
            float theZValue = gyroRotationEulerResult.z;
            gyroRotationEulerResult.z = gyroRotationEulerResult.x;
            gyroRotationEulerResult.x = theZValue;
        }

        
        rotationValueGyro.text = "Rotation gyro: " + gyro.attitude.eulerAngles;
        rotationValueObject.text = "Rotation result: " + gyroRotationEulerResult;
        return gyroRotationEulerResult;
    }
}
