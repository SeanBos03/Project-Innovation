using TMPro;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pushText;
    public Vector3 forceDirection = Vector3.forward;
    public float forceStrength = 10f;
    public bool isPushing = false;
     Rigidbody rb;
    [SerializeField] bool useForceImpulse;
    LoudnessRecorder theLoudnessRecorder;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        theLoudnessRecorder = GetComponent<LoudnessRecorder>();
    }

    void FixedUpdate()
    {
        if (theLoudnessRecorder.loudDetected || isPushing)
        {
            pushText.text = "Push: ON";
        }

        else
        {
            pushText.text = "Push: OFF";
        }

        if (theLoudnessRecorder.loudDetected || isPushing)
        {
            if (!useForceImpulse)
            {
                rb.AddForce(forceDirection.normalized * forceStrength, ForceMode.Force);
            }

            else
            {
                rb.AddForce(forceDirection.normalized * forceStrength, ForceMode.Impulse);
            }
        }
    }

    public void TogglePush()
    {
        isPushing = !isPushing;
    }
}
