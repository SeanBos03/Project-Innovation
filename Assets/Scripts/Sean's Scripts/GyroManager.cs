using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GyroManager : MonoBehaviour
{
    [SerializeField] bool debugOn;
    Gyroscope gyro;
    [SerializeField] TextMeshProUGUI phoneEulerDisplayText;
    [SerializeField] TextMeshProUGUI phoneQuaternionDisplay;
    bool gyroIsEnabled;
    Quaternion gyroRotation;

    /*
     * 0° = North
90° = East
180° = South
270° = West
    */

    void Start()
    {
        Input.compass.enabled = true;

        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroIsEnabled = true;
        }

        else
        {
            Debug.Log("Gyroscope not supported");
        }
    }
    void Update()
    {
        if (gyroIsEnabled)
        {
            gyroRotation = gyro.attitude;

            if (debugOn)
            {
                phoneEulerDisplayText.gameObject.SetActive(true);
                phoneQuaternionDisplay.gameObject.SetActive(true);
                phoneEulerDisplayText.text = "Phone rotation (Euler): " + gyro.attitude.eulerAngles;
                phoneQuaternionDisplay.text = "Compass: " + Input.compass.trueHeading;
            }    
        }

        if (!debugOn || !gyroIsEnabled)
        {
            phoneEulerDisplayText.gameObject.SetActive(false);
            phoneQuaternionDisplay.gameObject.SetActive(false);
        }
    }
    public Quaternion getGyroRotation()
    {
        return gyroRotation;
    }

    public bool gyroEnabled()
    {
        return gyroIsEnabled;
    }
}
