using UnityEngine;

//third person control functionality
public class CameraFollowSimple : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float cameraCollisionOffset = 0.2f; // Small offset to prevent complete overlap
    [SerializeField] float distance = 5f; // Desired distance from player
    [SerializeField] float height = 2f; // Desired height relative to player

    Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position; // Distance between camera and player at start
    }

    void LateUpdate()
    {
        // Get the player's position
        Vector3 playerPosition = player.transform.position;

        // Calculate the desired camera position based on the fixed offset
        Vector3 desiredPosition = playerPosition + offset;
            
        // Raycast to check for any wall between camera and player
        RaycastHit hit;
        if (Physics.Raycast(playerPosition, (desiredPosition - playerPosition).normalized, out hit, offset.magnitude))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                // If a wall is detected, adjust the camera position to prevent clipping
                desiredPosition = hit.point - (desiredPosition - playerPosition).normalized * cameraCollisionOffset;
            }
        }

        // Set the camera's position
        transform.position = desiredPosition;

        // Ensure the camera always looks at the player (keep the player centered)
        transform.LookAt(player.transform);
    }
}