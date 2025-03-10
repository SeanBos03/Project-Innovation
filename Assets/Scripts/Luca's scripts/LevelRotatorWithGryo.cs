using UnityEngine;

public class LevelRotatorWithGyro : MonoBehaviour
{
    public Transform emptyObject; // Assign the empty GameObject
    public Transform level; // Assign your level or scene root

    public float positionRotationSpeed = 10f; // Sensitivity for empty object's position
    public float gyroRotationSpeed = 1f; // Sensitivity for gyroscope rotation

    private Quaternion initialGyroRotation;

    void Start()
    {
        // Enable the gyroscope if available
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            initialGyroRotation = Input.gyro.attitude;
        }
    }

    void Update()
    {
        if (level == null) return;

        // **1. Rotation from Empty Object Position**
        float rotationX = emptyObject.position.y * positionRotationSpeed;
        float rotationY = emptyObject.position.x * positionRotationSpeed;

        Quaternion positionRotation = Quaternion.Euler(rotationX, rotationY, 0);

        // **2. Rotation from Gyroscope**
        Quaternion gyroRotation = Quaternion.identity;
        if (SystemInfo.supportsGyroscope)
        {
            gyroRotation = GyroToUnity(Input.gyro.attitude) * Quaternion.Inverse(initialGyroRotation);
        }

        // **3. Blend Both Rotations**
        level.rotation = Quaternion.Slerp(positionRotation, gyroRotation, gyroRotationSpeed * Time.deltaTime);
    }

    // Convert gyroscope rotation to Unity coordinates
    private Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
