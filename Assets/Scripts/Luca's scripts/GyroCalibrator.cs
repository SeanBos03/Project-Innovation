using UnityEngine;

public class GyroCalibrator : MonoBehaviour
{
    private Quaternion initialRotation;  // Store the initial rotation
    private bool calibrated = false;

    public Quaternion GetInitialRotation()
    {
        return initialRotation;
    }

    void Start()
    {
        // Wait a moment before calibrating to let the gyro stabilize
        Invoke("CalibrateGyro", 1.0f);  // Calibrate after 1 second
    }

    void CalibrateGyro()
    {
        // Capture the initial rotation of the object (should be zeroed out)
        initialRotation = transform.rotation;
        calibrated = true;
    }

    void Update()
    {
        // If calibrated, zero out the tilt to avoid shaking and unwanted rotation
        if (calibrated)
        {
            transform.rotation = Quaternion.Inverse(initialRotation) * transform.rotation;  // Neutralize tilt
        }
    }
}
