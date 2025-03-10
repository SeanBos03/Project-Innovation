using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEgg : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Transform player;  // Reference to the player
    [SerializeField] private Vector3 offset;  // Offset from the player (camera's relative position to the player)

    [Header("Gyroscope Settings")]
    [SerializeField] private float rotationSpeed = 5f;  // Rotation speed of the camera based on gyroscope input
    [SerializeField] private float moveSpeed = 5f;      // Movement speed of the player
    [SerializeField] private float smoothingFactor = 0.1f;  // Smoothing factor to reduce shake

    [Header("Swipe Settings")]
    [SerializeField] private float swipeSensitivity = 0.1f;  // Sensitivity of swipe to camera rotation

    private Quaternion baseRotation;  // Camera's initial rotation (relative to the player)
    private Quaternion currentRotation;  // Current rotation based on gyroscope input
    private Quaternion targetRotation;  // Target rotation after smoothing
    private Vector3 currentVelocity;    // Current velocity for movement
    private float gyroAngle = 0f;       // Current gyro angle (for movement direction)

    private Vector2 lastTouchPosition; // Stores the position of the last touch for swipe detection

    void Start()
    {
        // Enable the Gyroscope if the device supports it
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }

        baseRotation = transform.rotation;  // Store the initial rotation of the camera
    }

    void Update()
    {
        // Debugging: check if touch is being detected
        if (Input.touchCount > 0)
        {
            Debug.Log("Touch detected!");  // This will print whenever there is a touch.
        }

        // Update the camera's position to follow the player
        FollowPlayer();

        // Adjust the camera rotation based on gyroscope input
        AdjustCameraRotation();

        // Update the player's movement based on the gyroscope
        UpdatePlayerMovement();

        // Handle swipe rotation
        HandleSwipeRotation();
    }

    private void FollowPlayer()
    {
        // Camera follows the player with an offset, but doesn't rotate with them
        transform.position = player.position + offset;

        // Make sure the camera always faces the player
        Vector3 directionToPlayer = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(directionToPlayer);
    }

    private void AdjustCameraRotation()
    {
        // Get the current gyroscope attitude (rotation) but ignore the pitch (X axis) and roll (Z axis)
        Quaternion gyroRotation = Input.gyro.attitude;

        // The gyroscope gives the rotation in a different space, so we need to adjust it
        // Convert the quaternion from the device's space to the world space.
        // Invert the X and Z axes to fix the upside-down rotation.
        gyroRotation = new Quaternion(gyroRotation.x, gyroRotation.y, -gyroRotation.z, -gyroRotation.w);

        // Apply the gyroscope's rotation to the camera's rotation, but only around the Y axis
        currentRotation = Quaternion.Euler(0, gyroRotation.eulerAngles.y, 0);

        // Smoothly interpolate towards the target rotation to avoid jittering or shaking
        targetRotation = Quaternion.Slerp(transform.rotation, currentRotation, smoothingFactor * Time.deltaTime);

        // Apply the smoothed rotation
        transform.rotation = targetRotation;
    }

    private void UpdatePlayerMovement()
    {
        // Get the gyroscope's attitude to calculate the player's movement direction
        Quaternion gyroRotation = Input.gyro.attitude;

        // Convert the gyroscope's rotation to Euler angles (so we can work with them)
        Vector3 gyroEuler = gyroRotation.eulerAngles;

        // Calculate the angle the player has rotated, and adjust the movement direction accordingly
        gyroAngle = gyroEuler.y;  // Use the y-axis for left-right rotation (horizontal rotation)

        // Convert the gyroscope angle to a movement direction relative to the player's rotation
        Vector3 moveDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * gyroAngle), 0f, Mathf.Cos(Mathf.Deg2Rad * gyroAngle));

        // Apply the movement in the direction the player is facing
        player.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private void HandleSwipeRotation()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // If it's the first touch, store the initial position
            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                Debug.Log("Touch Started at: " + touch.position);  // Debugging touch start position
            }

            // If the touch is moving, calculate the swipe delta
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPosition = touch.position - lastTouchPosition;

                // Debugging: log the swipe delta (for visual inspection)
                Debug.Log("Swipe delta: " + deltaPosition);

                // Rotate the camera based on swipe delta (adjust sensitivity as needed)
                float deltaRotation = deltaPosition.x * swipeSensitivity;

                // Apply the rotation to the camera's yaw (around the Y axis)
                currentRotation *= Quaternion.Euler(0, -deltaRotation, 0);

                // Smoothly interpolate the camera's rotation
                targetRotation = Quaternion.Slerp(transform.rotation, currentRotation, smoothingFactor * Time.deltaTime);

                // Apply the rotation
                transform.rotation = targetRotation;

                // Update the last touch position for the next frame
                lastTouchPosition = touch.position;
            }
        }
        else if (Application.isEditor && Input.GetMouseButton(0))  // Simulate swipe for testing in the editor
        {
            Vector2 touchPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Touch simulatedTouch = new Touch();
            simulatedTouch.position = touchPosition;
            simulatedTouch.phase = TouchPhase.Moved;
            HandleSwipeRotation(simulatedTouch);
        }
    }

    private void HandleSwipeRotation(Touch touch)
    {
        // If it's the first touch, store the initial position
        if (touch.phase == TouchPhase.Began)
        {
            lastTouchPosition = touch.position;
            Debug.Log("Touch Started at: " + touch.position);  // Debugging touch start position
        }

        // If the touch is moving, calculate the swipe delta
        if (touch.phase == TouchPhase.Moved)
        {
            Vector2 deltaPosition = touch.position - lastTouchPosition;

            // Debugging: log the swipe delta (for visual inspection)
            Debug.Log("Swipe delta: " + deltaPosition);

            // Rotate the camera based on swipe delta (adjust sensitivity as needed)
            float deltaRotation = deltaPosition.x * swipeSensitivity;

            // Apply the rotation to the camera's yaw (around the Y axis)
            currentRotation *= Quaternion.Euler(0, -deltaRotation, 0);

            // Smoothly interpolate the camera's rotation
            targetRotation = Quaternion.Slerp(transform.rotation, currentRotation, smoothingFactor * Time.deltaTime);

            // Apply the rotation
            transform.rotation = targetRotation;

            // Update the last touch position for the next frame
            lastTouchPosition = touch.position;
        }
    }
}


