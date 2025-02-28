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
    [SerializeField] private Vector3 minRotationBig;
    [SerializeField] private Vector3 maxRotationBig;


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

            if (includeX)
            {
                //gyroEuler.x = Mathf.Clamp(gyroEuler.x, minRotation.x, maxRotation.x);
                gyroEuler.x = RotationClamp(gyroEuler.x, minRotation.x, maxRotation.x,
                    minRotationBig.x, maxRotationBig.x);
            }
            else
            {
                gyroEuler.x = 0;
            }

            if (includeY)
            {
                gyroEuler.y = RotationClamp(gyroEuler.y, minRotation.y, maxRotation.y,
                    minRotationBig.y, maxRotationBig.y);
            }
            else
            {
                gyroEuler.y = 0;
            }

            if (includeZ)
            {
                gyroEuler.z = RotationClamp(gyroEuler.z, minRotation.z, maxRotation.z,
                    minRotationBig.z, maxRotationBig.z);
            }
            else
            {
                gyroEuler.z = 0;
            }

            Quaternion resultRotation = Quaternion.Euler(gyroEuler);

            if (yAndZSwitch)
            {
                float zValue = resultRotation.z;
                resultRotation.z = resultRotation.y;
                resultRotation.y = zValue;
            }

            transform.localRotation = Quaternion.Lerp(transform.localRotation, resultRotation * baseDirection, Time.deltaTime * rotationSpeed);
        }
    }

    //angle should be values between 0 - 360
    float RotationClamp(float angle, float min, float max, float min1, float max2)
    {
        if (angle < 180)
        {
            return Mathf.Clamp(angle, min, max);
        }

        return Mathf.Clamp(angle, min1, max2);

        //bool isNegative = false;

        //if (angle > 180)
        //{
        //    angle -= 360;
        //}

        //if (angle < 0)
        //{
        //    isNegative = true;
        //    angle *= -1;
        //}

        //if (angle < min)
        //{
        //    angle = min;
        //}

        //if (angle > max)
        //{
        //    angle = max;
        //}

        //if (isNegative)
        //{
        //    return angle * -1;
        //}

        //return angle;
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