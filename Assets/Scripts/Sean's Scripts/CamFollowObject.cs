using UnityEngine;

public class CamFollowObject : MonoBehaviour
{
    public Transform player;
    public float swipeSpeed = 0.2f;
    public float zoomSpeed = 0.5f;
    public float pinchThreshold = 5f; // Minimum pinch movement required to trigger zoom

    public float minRotationY = -30f, maxRotationY = 30f; // Horizontal rotation limits
    public float minRotationX = -20f, maxRotationX = 20f; // Vertical rotation limits
    public float minZoom = 2f, maxZoom = 10f; // Zoom limits

    public float initialZoom = 5f; // Desired initial zoom level

    private Vector3 offset;
    private float currentRotationY;
    private float currentRotationX;
    private float zoomDistance;

    private Vector2 lastTouchPosition;
    private bool isSwiping = false;
    private float lastPinchDistance;
    private bool isZooming = false;

    Quaternion initialRotation;

    [SerializeField] bool affectByturtorial;

    void Start()
    {
        offset = transform.position - player.position;
        zoomDistance = Mathf.Clamp(initialZoom, minZoom, maxZoom); // Set initial zoom level

        initialRotation = transform.rotation;
    }

    void Update()
    {
        HandleTouchInput();
    }

    void LateUpdate()
    {
        Vector3 zoomedOffset = offset.normalized * zoomDistance;
        transform.position = player.position + zoomedOffset;

        Quaternion horizontalRotation = Quaternion.Euler(0f, currentRotationY, 0f);
        Quaternion verticalRotation = Quaternion.Euler(currentRotationX, 0f, 0f);
        transform.rotation = initialRotation * horizontalRotation * verticalRotation;
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            HandleSingleTouch();
        }
        else if (Input.touchCount == 2)
        {
            HandleTwoFingerZoom();
        }
    }

    void HandleSingleTouch()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            lastTouchPosition = touch.position;
            isSwiping = true;
        }
        else if (touch.phase == TouchPhase.Moved && isSwiping && !GameData.swipeLock)
        {
            if (affectByturtorial)
            {
                if (GameData.TurtorialStage == 5)
                {
                    Invoke("Continueturtorial", 2f);
                }
            }

            Vector2 delta = touch.position - lastTouchPosition;
            lastTouchPosition = touch.position;

            currentRotationY += delta.x * swipeSpeed; // Left/right rotation
            currentRotationX -= delta.y * swipeSpeed; // Up/down rotation

            currentRotationY = Mathf.Clamp(currentRotationY, minRotationY, maxRotationY);
            currentRotationX = Mathf.Clamp(currentRotationX, minRotationX, maxRotationX);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            isSwiping = false;
        }
    }

    void Continueturtorial()
    {
        GameData.TurtorialStage = 6;
    }

    void HandleTwoFingerZoom()
    {
        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        float pinchDistance = Vector2.Distance(touch1.position, touch2.position);

        if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
        {
            lastPinchDistance = pinchDistance;
            isZooming = false;
        }
        else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
        {
            float pinchDelta = pinchDistance - lastPinchDistance;
            lastPinchDistance = pinchDistance;

            // Only zoom if the pinch movement is above threshold
            if (Mathf.Abs(pinchDelta) > pinchThreshold)
            {
                isZooming = true;
            }

            if (isZooming)
            {
                zoomDistance -= pinchDelta * zoomSpeed;
                zoomDistance = Mathf.Clamp(zoomDistance, minZoom, maxZoom);
            }
        }
        else if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
        {
            isZooming = false; // When fingers lifted, not zooming
        }
    }

    public void ResetCamera()
    {
        currentRotationY = 0f;
        currentRotationX = 0f;
        zoomDistance = Mathf.Clamp(initialZoom, minZoom, maxZoom); // Reset to initial zoom level
    }
}
