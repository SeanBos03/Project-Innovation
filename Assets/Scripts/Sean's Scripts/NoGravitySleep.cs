using TMPro;
using UnityEngine;

public class NoGravitySleep : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float stickTime = 0.1f; //short buffer to prevent object getting stuck at collision
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0f;
    }

    //prevents pass through other collider at high speed
    void FixedUpdate()
    {
        if (rb.velocity.magnitude > 0)
        {
            if (GameData.shouldStick)
            {
                //look at by script
                RaycastHit hit;
                Vector3 moveDirection = rb.velocity.normalized; //making sure it's pure direction
                float moveDistance = rb.velocity.magnitude * Time.fixedDeltaTime + stickTime; //the distance object will move

                //check if there is an obstacle ahead
                if (Physics.Raycast(transform.position, moveDirection, out hit, moveDistance))
                {
                    rb.position = hit.point - moveDirection * stickTime; //stop at collision
                    rb.velocity = Vector3.zero;
                }
            }
            
        }
    }
}
