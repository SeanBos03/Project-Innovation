using UnityEngine;

public class PushObject : MonoBehaviour
{
    public Vector3 forceDirection = Vector3.forward;
    public float forceStrength = 10f;
    public bool isPushing = false;
     Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isPushing)
        {
            rb.AddForce(forceDirection.normalized * forceStrength, ForceMode.Force);
        }
    }

    public void TogglePush()
    {
        isPushing = !isPushing;
    }
}
