using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject settingsMenu; // Drag the Settings Panel here in the Inspector
    public GameObject settingsButton; // Drag the Settings Button here in the Inspector
    public GameObject closeButton; // Drag the Close Button here in the Inspector

    private void Start()
    {
        // Ensure the settings menu is hidden at the start
        settingsMenu.SetActive(false);

        // Add listeners for buttons
        settingsButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OpenSettingsMenu);
        closeButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(CloseSettingsMenu);
    }

    // Function to open the settings menu
    void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    // Function to close the settings menu
    void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }
}
