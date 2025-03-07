using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoateObjectGyroscopeExperimental : MonoBehaviour
{
    Gyroscope gyro;
    bool gyroEnabled; // is gyroscope available or not
    Quaternion rotationAdjustment; // adjust measurement for landscape mode
    [SerializeField] TextMeshProUGUI rotationValueGyro;
    [SerializeField] TextMeshProUGUI rotationValueObject;
    [SerializeField] TextMeshProUGUI rotationValueResult;
    [SerializeField] TextMeshProUGUI rotationStatus;
    bool isReady;
    bool starterTimerOver;

    [SerializeField] float timeBeforeStart = 2f;
    [SerializeField] float startXThresholdValue;
    [SerializeField] float startZThresholdValue;
    [SerializeField] float rotateSpeed = 1.5f;
    [SerializeField] float xMaxValue;
    [SerializeField] float zMaxValue;

    [SerializeField] bool invertX;
    [SerializeField] bool invertZ;
    [SerializeField] bool switchValues;

    [SerializeField] GameObject referenceObject; // Newly added game object to set rotation perspective

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
        if (!gyroEnabled || referenceObject == null)
        {
            return;
        }

        Vector3 result = CalculateRotation();

        if (!isReady)
        {
            if (starterTimerOver)
            {
                if (Mathf.Abs(result.x) <= startXThresholdValue && Mathf.Abs(result.z) <= startZThresholdValue)
                {
                    isReady = true;
                    rotationStatus.text = "Start rotating";
                }
            }
        }

        if (isReady)
        {
            // Apply rotation relative to the reference object
            Quaternion targetRotation = referenceObject.transform.rotation * Quaternion.Euler(result);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * rotateSpeed);
            rotationValueResult.text = "Rotation object: " + result;
        }
    }

    // Adjust gyroscope values based on the reference object's orientation
    Vector3 CalculateRotation()
    {
        // Compute rotation relative to reference object
        Quaternion relativeRotation = Quaternion.Inverse(referenceObject.transform.rotation) * gyro.attitude;
        Quaternion rotationAdjustment = Quaternion.Euler(0, 0, 90) * Quaternion.Inverse(relativeRotation);

        rotationValueResult.text = "RotationAdjustment: " + rotationAdjustment.eulerAngles;
        Vector3 rotationAdjustmentAdjusted = rotationAdjustment.eulerAngles;

        // Adjust x-axis values
        if (rotationAdjustmentAdjusted.x > 180)
        {
            rotationAdjustmentAdjusted.x = 360f - rotationAdjustmentAdjusted.x;
            rotationAdjustmentAdjusted.x = rotationAdjustmentAdjusted.x * -1;
        }

        // Adjust y-axis values
        if (rotationAdjustmentAdjusted.y > 180)
        {
            rotationAdjustmentAdjusted.y = 360f - rotationAdjustmentAdjusted.y;
        }
        else
        {
            rotationAdjustmentAdjusted.y *= -1;
        }

        // Switch y and z
        Vector3 gyroRotationEulerResult = rotationAdjustmentAdjusted;
        gyroRotationEulerResult.z = gyroRotationEulerResult.y;
        gyroRotationEulerResult.y = 0;

        // Limit values
        bool xIsNegative = gyroRotationEulerResult.x < 0;
        bool zIsNegative = gyroRotationEulerResult.z < 0;

        gyroRotationEulerResult.x = Mathf.Abs(gyroRotationEulerResult.x);
        gyroRotationEulerResult.z = Mathf.Abs(gyroRotationEulerResult.z);

        if (gyroRotationEulerResult.x > xMaxValue) gyroRotationEulerResult.x = xMaxValue;
        if (gyroRotationEulerResult.z > zMaxValue) gyroRotationEulerResult.z = zMaxValue;

        if (xIsNegative) gyroRotationEulerResult.x *= -1;
        if (zIsNegative) gyroRotationEulerResult.z *= -1;

        // Apply inversions and switches
        if (invertX) gyroRotationEulerResult.x *= -1;
        if (invertZ) gyroRotationEulerResult.z *= -1;
        if (switchValues)
        {
            float temp = gyroRotationEulerResult.z;
            gyroRotationEulerResult.z = gyroRotationEulerResult.x;
            gyroRotationEulerResult.x = temp;
        }

        rotationValueGyro.text = "Rotation gyro: " + gyro.attitude.eulerAngles;
        rotationValueObject.text = "Rotation result: " + gyroRotationEulerResult;
        return gyroRotationEulerResult;
    }
}