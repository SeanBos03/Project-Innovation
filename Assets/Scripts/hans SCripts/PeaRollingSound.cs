using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaRollingSound : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private AudioClip rollingSound;
    private Rigidbody rb;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float velocityMagnitude = rb.velocity.magnitude;

        if (velocityMagnitude > 0.1f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = rollingSound;
                audioSource.loop = true;
                audioSource.Play();
            }
            audioSource.pitch = Mathf.Lerp(0.5f, 1.0f, velocityMagnitude / 5f);
        }
        else
        {
            audioSource.Stop();
        }
    }
}
