using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GyroInstructionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rotationStatus;  // Rotation status text
    [SerializeField] private GameObject gyroInstructionPanel; // Panel for "Rotate phone to good orientation"
    [SerializeField] private GameObject startRotatingPanel;   // Panel for "Start rotating" message
    [SerializeField] private float startRotatingDuration = 3f; // Duration to show the "Start rotating" panel

    private bool isStartRotatingPanelShown = false; // Flag to check if the "Start rotating" panel is already shown
    private bool hasStartedRotating = false;         // Flag to ensure the panel is only triggered once

    private void Start()
    {
        // Ensure both panels are hidden at the start
        gyroInstructionPanel.SetActive(false);
        startRotatingPanel.SetActive(false);
    }

    private void Update()
    {
        // Show the "Rotate phone to good orientation" panel if the status says that
        if (rotationStatus.text == "Rotate phone to good orientation")
        {
            gyroInstructionPanel.SetActive(true);  // Show instruction panel
        }
        else
        {
            gyroInstructionPanel.SetActive(false); // Hide instruction panel when not needed
        }

        // Show the "Start rotating" panel only if it's not already shown
        if (rotationStatus.text == "Start rotating" && !hasStartedRotating)
        {
            // Trigger the panel to show and start the timer to hide it
            StartCoroutine(ShowAndHideStartRotatingPanel());
            hasStartedRotating = true;  // Prevent the panel from showing again
        }
    }

    // Coroutine to show the "Start rotating" panel for a set duration (3 seconds)
    private IEnumerator ShowAndHideStartRotatingPanel()
    {
        // Set the flag to true to prevent showing the panel again
        isStartRotatingPanelShown = true;

        // Show the "Start rotating" panel
        startRotatingPanel.SetActive(true);

        // Wait for the specified duration (3 seconds)
        yield return new WaitForSeconds(startRotatingDuration);

        // Hide the panel after the delay
        startRotatingPanel.SetActive(false);

        // Reset the flag to allow the panel to show again if needed
        isStartRotatingPanelShown = false;
        hasStartedRotating = false; // Allow the panel to show again if the status is "Start rotating" later
    }
}
