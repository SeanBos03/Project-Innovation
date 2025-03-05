using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public string playerName = "Player"; // Name of the player GameObject, modify if needed
    public float disappearDelay = 1f;   // Delay before the plate disappears

    private void OnCollisionEnter(Collision collision)
    {
        // Debug log to check what is colliding with the plate
        Debug.Log("Collision detected: " + collision.gameObject.name);

        // Check if the object colliding with the plate is the player
        if (collision.gameObject.name == playerName)
        {
            Debug.Log("Player detected! Plate will disappear in " + disappearDelay + " seconds.");
            StartCoroutine(DisappearAfterDelay());
        }
        else
        {
            Debug.Log("Non-player object collided: " + collision.gameObject.name);
        }
    }

    IEnumerator DisappearAfterDelay()
    {
        // Wait for the delay before destroying the plate
        yield return new WaitForSeconds(disappearDelay);
        Debug.Log("Pressure plate destroyed!");
        Destroy(gameObject); // Destroy the pressure plate
    }
}


