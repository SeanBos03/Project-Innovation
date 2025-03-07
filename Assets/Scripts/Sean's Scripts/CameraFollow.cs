using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float rotationSpeed = 0.2f;
    Vector3 offset;
    float rotationY; //yaw of the cam
    float rotationX; //pitch of the cam
    //no rotationZ because roll doesn't need to be modified

    [SerializeField] float minSwipeAngle = -45;
    [SerializeField] float maxSwipeAngle = 75;
    [SerializeField] bool invertVerticalMovement;
    [SerializeField] float cameraCollisionOffset = 0.2f; // Offset to keep the camera slightly away from walls

    // New feature: Reset camera to specific rotation
    Vector3 targetRotation;
    bool resetRotation = false; // Flag to track if the reset is triggered

    void Start()
    {
        offset = transform.position - player.transform.position; //distance between the camera and the player.
        Vector3 angles = transform.eulerAngles;
        rotationY = angles.y;
        rotationX = angles.x;
    }

    void LateUpdate()
    {
        if (resetRotation) // If the reset flag is set, reset the rotation
        {
            rotationX = targetRotation.x;
            rotationY = targetRotation.y;
            resetRotation = false; // Reset the flag after the reset is applied
        }

        if (Input.touchCount == 1) //check if there is only one finger swipe
        {
            Touch touch = Input.GetTouch(0); //touch(0) sees the player swipe with the first finger
            if (touch.phase == TouchPhase.Moved) //check if the player is swiping
            {
                rotationY += touch.deltaPosition.x * rotationSpeed; //adjust y-axis rotation (horizontal movement) based on player swipe's X-axis movement.
                if (!invertVerticalMovement)
                {
                    rotationX += touch.deltaPosition.y * rotationSpeed; //swipe down rotates down
                }
                else
                {
                    rotationX -= touch.deltaPosition.y * rotationSpeed; //swipe down rotates up
                }

                rotationX = Mathf.Clamp(rotationX, minSwipeAngle, maxSwipeAngle); //make sure angle isn't too extreme
            }
        }

        Quaternion newCamRotation = Quaternion.Euler(rotationX, rotationY, 0); //using updated rotationX and rotationY to determine new camera orientation.
        Vector3 newCamPosition = player.transform.position + newCamRotation * offset; //finds the new position the cam will be

        //check for wall collision between player and cam
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, (newCamPosition - player.transform.position).normalized, out hit, offset.magnitude))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                transform.position = hit.point + hit.normal * cameraCollisionOffset; //moves cam away from the wall using the direction pointing out from the collided wall
            }
            else
            {
                transform.position = newCamPosition; //doesn't collide with wall, so continue with new position
            }
        }
        else
        {
            transform.position = newCamPosition; //no collision detected
        }

        transform.LookAt(player.transform.position); //make the camera always face the player.
    }

    // Method to trigger the camera rotation reset
    public void ResetCameraRotation()
    {
        targetRotation = GameData.mainCamDeaultRotation.eulerAngles;
        resetRotation = true;
    }
}