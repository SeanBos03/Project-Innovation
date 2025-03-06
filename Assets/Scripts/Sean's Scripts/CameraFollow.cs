using UnityEngine;

//thrid person control functionality
//camera will maintain some distance away from the player and player can swipe the screen to rotate the camera
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

    void Start()
    {
        offset = transform.position - player.transform.position; //distance between the camera and the player.
        Vector3 angles = transform.eulerAngles; 
        rotationY = angles.y;
        rotationX = angles.x;
    }

    void LateUpdate()
    {
        if (Input.touchCount == 1) //check if there is only one finger swipe
        {
            Touch touch = Input.GetTouch(0); //toouch(0) sees the player swipe with the first finger
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

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.position = player.transform.position + rotation * offset;
        transform.LookAt(player.transform.position); // the camera always faces the player.
    }
}

//using UnityEngine;

////for achieving the thrid person effect
//public class CameraFollow : MonoBehaviour
//{
//    public GameObject player;
//    Vector3 initialOffset;

//    void Start()
//    {
//        initialOffset = transform.position - player.transform.position;
//    }
//    void LateUpdate()
//    {
//        transform.position = player.transform.position + initialOffset;
//    }
//}