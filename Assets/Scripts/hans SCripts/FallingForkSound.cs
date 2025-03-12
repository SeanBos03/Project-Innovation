using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingForkSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] forkSounds;
    private Rigidbody rb;
    private bool hasPlayedSound = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!hasPlayedSound && rb.velocity.y < -0.1f)
        {
            PlayRandomForkSound();
            hasPlayedSound = true;
        }
    }

    private void PlayRandomForkSound()
    {
        if (forkSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, forkSounds.Length);
            audioSource.PlayOneShot(forkSounds[randomIndex]);
        }
    }
}
