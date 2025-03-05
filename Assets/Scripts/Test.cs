using UnityEngine;

public class Test : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroEnabled;

    [Header("Enable Rotation on Axes")]
    public bool applyX = true;
    public bool applyY = true;
    public bool applyZ = true;

    void Start()
    {
        // Check if gyroscope is available
        gyroEnabled = SystemInfo.supportsGyroscope;

        if (gyroEnabled)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }
    }

    void Update()
    {
        if (gyroEnabled)
        {
            ApplyGyroRotation();
        }
        else
        {
            ApplyAccelerometerRotation();
        }
    }

    void ApplyGyroRotation()
    {
        // Get gyroscope rotation
        Quaternion deviceRotation = gyro.attitude;

        // Convert to Unity coordinate system
        Quaternion correctRotation = new Quaternion(deviceRotation.x, deviceRotation.y, -deviceRotation.z, -deviceRotation.w);

        // Extract individual axis values
        Vector3 euler = correctRotation.eulerAngles;

        // Apply only selected axes
        float rotX = applyX ? euler.x : transform.eulerAngles.x;
        float rotY = applyY ? euler.y : transform.eulerAngles.y;
        float rotZ = applyZ ? euler.z : transform.eulerAngles.z;

        // Set final rotation
        transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
    }

    void ApplyAccelerometerRotation()
    {
        Vector3 acceleration = Input.acceleration;

        // Convert acceleration to rotation values
        float tiltX = applyX ? acceleration.x * 90f : transform.eulerAngles.x;
        float tiltY = applyY ? acceleration.y * 90f : transform.eulerAngles.y;
        float tiltZ = applyZ ? acceleration.z * 90f : transform.eulerAngles.z;

        // Apply rotation
        transform.rotation = Quaternion.Euler(tiltY, -tiltX, -tiltZ);
    }
}