using UnityEngine;

public class CamFollowStatic : MonoBehaviour
{
    public Transform player;
    public float swipeSpeed = 0.2f;

    public float minRotationY = -30f, maxRotationY = 30f; // Horizontal rotation limits
    public float minRotationX = -20f, maxRotationX = 20f; // Vertical rotation limits

    private Vector3 offset;
    private float currentRotationY;
    private float currentRotationX;

    private Vector2 lastTouchPosition;
    private bool isSwiping = false;

    private Quaternion initialRotation; // Stores the starting rotation of the camera

    void Start()
    {
        // Store the initial offset from the player
        offset = transform.position - player.position;

        // Store the initial rotation as a reference frame
        initialRotation = transform.rotation;

        // Convert initial rotation to Euler angles
        Vector3 startEuler = initialRotation.eulerAngles;


    }

    void Update()
    {
        HandleSwipe();
    }

    void LateUpdate()
    {
        // Keep the camera at the same offset from the player
        transform.position = player.position + offset;

        // Apply rotation relative to the initial rotation
        Quaternion horizontalRotation = Quaternion.Euler(0f, currentRotationY, 0f);
        Quaternion verticalRotation = Quaternion.Euler(currentRotationX, 0f, 0f);
        transform.rotation = initialRotation * horizontalRotation * verticalRotation;
    }

    void HandleSwipe()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                Vector2 delta = touch.position - lastTouchPosition;
                lastTouchPosition = touch.position;

                // Adjust rotation based on swipe direction
                currentRotationY += delta.x * swipeSpeed; // Horizontal swipe (left/right)
                currentRotationX -= delta.y * swipeSpeed; // Vertical swipe (up/down)

                // Clamp rotation within limits
                currentRotationY = Mathf.Clamp(currentRotationY, minRotationY, maxRotationY);
                currentRotationX = Mathf.Clamp(currentRotationX, minRotationX, maxRotationX);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isSwiping = false;
            }
        }
    }
}