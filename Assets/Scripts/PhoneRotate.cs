using UnityEngine;

public class PhoneRotate : MonoBehaviour
{
    Gyroscope gyro;
    bool gyroEnabled;
    Quaternion rotationFix;
   // [SerializeField] GameObject theObject;

    void Start()
    {
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            rotationFix = new Quaternion(0, 0, 1, 0);
            return true;
        }
        Debug.Log("Gyroscope not supported");


        return false;
    }

    void Update()
    {
        if (gyroEnabled)
        {
            transform.rotation = gyro.attitude * rotationFix;
        }
    }
}