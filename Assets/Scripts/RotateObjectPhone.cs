using TMPro;
using UnityEngine;
public class RotateObjectPhone : MonoBehaviour
{
    Gyroscope gyro;
    bool gyroEnabled;
    Quaternion rotationFix;
    Quaternion initialRotation;
    public GameObject theObject;
    public TMP_Text phoneEulerDisplayText;
    public TMP_Text phoneQuaternionDisplay;
    public TMP_Text phoneRotationDisplay;

    // Define a threshold for determining portrait orientation (this might need tweaking)
    float portraitThreshold = 0.9f;

    void Start()
    {
        gyroEnabled = EnableGyro();
        initialRotation = transform.rotation;  // Store the initial rotation when the app starts
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            ////gyroscope's crood system has z points upward, but unity crood system has z points forward
            rotationFix = Quaternion.identity; 
            return true;
        }
        return false;
    }

    void Update()
    {
        Quaternion theResult = gyro.attitude * rotationFix;
        Vector3 theAngle = initialRotation.eulerAngles;
        theAngle.x = theResult.eulerAngles.y;
     // theObject.transform.localRotation = initialRotation;
     //initialRotation
         theObject.transform.rotation = Quaternion.Euler(theAngle);

        phoneEulerDisplayText.text = "Phone rotation (Euler): " + gyro.attitude.eulerAngles;
        phoneQuaternionDisplay.text = "Phone rotation (Quaternion): " + gyro.attitude;
        phoneRotationDisplay.text = "Object rotation: " + gyro.attitude * rotationFix;


        //if (gyroEnabled)
        //{
        //    // Check if the phone is in portrait mode (with some tolerance)
        //    if (Mathf.Abs(gyro.rotationRateUnbiased.x) < portraitThreshold &&
        //        Mathf.Abs(gyro.rotationRateUnbiased.y) < portraitThreshold &&
        //        Mathf.Abs(gyro.rotationRateUnbiased.z) < portraitThreshold)
        //    {
        //        // Reset the rotation to the initial rotation (0,0,0)
        //        transform.rotation = initialRotation;
        //    }
        //    else
        //    {
        //        // Apply the gyroscope's attitude, adjusting for Unity's coordinate system
        //        transform.localRotation = gyro.attitude * rotationFix;
        //    }
    }
}