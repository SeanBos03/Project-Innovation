using TMPro;
using UnityEngine;

public class NoGravitySleep : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float skinWidth = 0.1f; //short buffer to prevent object getting stuck at collision
    [SerializeField] TextMeshProUGUI stickText;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0f;
    }

    //prevents pass through other collider at high speed
    void FixedUpdate()
    {
        if (GameData.shouldStick)
        {
            stickText.text = "Stick: ON";
        }

        else
        {
            stickText.text = "Stick: OFF";
            return;
        }


        if (rb.velocity.magnitude > 0)
        {
            //i dont know but i've been told that gyroscope isn't worth the woe
            //look at by script
            RaycastHit hit;
            Vector3 moveDirection = rb.velocity.normalized; //making sure it's pure direction
            float moveDistance = rb.velocity.magnitude * Time.fixedDeltaTime + skinWidth; //the distance object will move

            //check if there is an obstacle ahead
            if (Physics.Raycast(transform.position, moveDirection, out hit, moveDistance))
            {
                rb.position = hit.point - moveDirection * skinWidth; //stop at collision
                rb.velocity = Vector3.zero;
            }
        }
    }
    public void ToggleStick()
    {
        GameData.shouldStick = !GameData.shouldStick;
    }
}
