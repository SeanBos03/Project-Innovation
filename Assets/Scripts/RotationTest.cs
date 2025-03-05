using UnityEngine;

public class RotationTest : MonoBehaviour
{
    private Quaternion initialRotation; // Stores the starting orientation

    void Start()
    {
        Input.gyro.enabled = true; // Enable the gyroscope

        // Store the initial rotation so that we can compensate for it
        initialRotation = Quaternion.Inverse(Input.gyro.attitude);
    }

    void Update()
    {
        // Get the current gyroscope rotation
        Quaternion gyroRotation = Input.gyro.attitude;

        // Apply it to the platform, compensating for initial rotation
        transform.rotation = Quaternion.Euler(90, 0, 0) * Quaternion.Inverse(initialRotation) * gyroRotation;
    }
}