using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class GyroRotate : MonoBehaviour
{
    [SerializeField] Quaternion baseDirection = Quaternion.identity; //0,0,1,0
    [SerializeField] GyroManager gyroManager;
    [SerializeField] bool includeX = true;
    [SerializeField] bool includeY = true;
    [SerializeField] bool includeZ = true;
    [SerializeField] bool hasStartThreshold;
    [SerializeField] Vector3 bigThreshold;//euler
    [SerializeField] Vector3 smallThreshold;//euler
    bool hasStarted = false;
    bool timerOver = false;
    [SerializeField] TextMeshProUGUI textaaa;
    [SerializeField]  bool yAndZSwitch = false;

    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Vector3 maxRotation; //max allowed rotation in an axis
    [SerializeField] private Vector3 minRotation; //mininum allowed rotation in an an axis
    void Start()
    {
        if (!hasStartThreshold)
        {
            hasStarted = true;
        }

        StartCoroutine(RotateTimer());
    }

    IEnumerator RotateTimer()
    {
        yield return new WaitForSeconds(2.5f);
        timerOver = true;
    }

    void Update()
    {
        if (!timerOver)
        {
            textaaa.text = "Stating...";
            return;
        }

        if (!gyroManager.gyroEnabled())
        {
            return;
        }

        Quaternion gyroRotation = gyroManager.getGyroRotation();
        Vector3 gyroEuler = gyroRotation.eulerAngles;

        if (!hasStarted)
        {
            bool xGood = true;
            bool yGood = true;
            bool zGood = true;
            textaaa.text = "Rotate the phone to a good orientation";
            if (includeX)
            {
                xGood = IsWithinThreshold(gyroEuler.x, bigThreshold.x, smallThreshold.x);
            }
            if (includeY)
            {
                xGood = IsWithinThreshold(gyroEuler.y, bigThreshold.y, smallThreshold.y);
            }
            if (includeZ)
            {
                xGood = IsWithinThreshold(gyroEuler.z, bigThreshold.z, smallThreshold.z);
            }

            if (xGood && yGood && zGood)
            {
                textaaa.text = "All done";
                hasStarted = true;
            }
            else
            {
                return;
            }
        }

        if (hasStarted)
        {
        
            if (!includeX)
            {
                gyroEuler.x = 0;
            }
            if (!includeY)
            {
                gyroEuler.y = 0;
            }
            if (!includeZ)
            {
                gyroEuler.z = 0;
            }

            Quaternion result = Quaternion.Euler(gyroEuler);

            if (yAndZSwitch)
            {
                float zValue = result.z;
                result.z = result.y;
                result.y = zValue;
            }
            transform.localRotation = result * baseDirection;
        }
    }

    bool IsWithinThreshold(float angle, float big, float small)
    {
        angle = ValidAngle(angle);
        big = ValidAngle(big);
        small = ValidAngle(small);

        if (angle > big
            || angle < small)
        {
            return true;
        }
        return false;
    }

    //make it so the angle is within 0-360 degree range
    float ValidAngle(float angle)
    {
        while (angle < 0) angle += 360; //make sure no negative angle
        while (angle >= 360) angle -= 360; //make sure angle isn't over 360
        return angle;
    }
}