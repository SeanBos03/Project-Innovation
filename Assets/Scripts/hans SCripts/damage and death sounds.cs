using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip rollSound;
    private PlayerScript playerScript;
    private Rigidbody rb;
    private int lastLife;

    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        rb = GetComponent<Rigidbody>();
        lastLife = GameData.life;
    }

    void Update()
    {
        if (GameData.life < lastLife)
        {
            if (GameData.life > 0)
            {
                PlaySound(damageSound);
            }
            else
            {
                PlaySound(deathSound);
            }
        }
        lastLife = GameData.life;

        float velocityMagnitude = rb.velocity.magnitude;
        if (velocityMagnitude > 0.1f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = rollSound;
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

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

