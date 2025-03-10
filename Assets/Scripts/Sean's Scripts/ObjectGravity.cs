using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public float gravityMultiplier = 9.81f; //Earth's gravity by default
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable default gravity
    }

    void FixedUpdate()
    {
        Vector3 gravity = gravityMultiplier * Physics.gravity;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}