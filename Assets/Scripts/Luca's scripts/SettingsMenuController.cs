using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject settingsButton;
    [SerializeField] GameObject closeButton;

    void Start()
    {
        settingsButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OpenSettingsMenu);
        closeButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(CloseSettingsMenu);
    }
    void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }
}
