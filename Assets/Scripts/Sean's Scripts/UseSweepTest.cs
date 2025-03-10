using UnityEngine;

public class UseSweepTest : MonoBehaviour
{
    [SerializeField] float collisionCheckDistance = 5.0f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 direction = transform.forward;

        if (rb.SweepTest(direction, out hit, collisionCheckDistance))
        {
        }
    }
}