using UnityEngine;

//third person control functionality
public class CameraFollowSimple : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float cameraCollisionOffset = 0.2f; // Small offset to prevent complete overlap
    Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position; //distance between the camera and the player.
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 direction = desiredPosition - player.transform.position;
        float distance = direction.magnitude;
        direction.Normalize();

        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, direction, out hit, distance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                transform.position = hit.point - (direction * cameraCollisionOffset);
                return;
            }
        }

        transform.position = desiredPosition;
    }
}
