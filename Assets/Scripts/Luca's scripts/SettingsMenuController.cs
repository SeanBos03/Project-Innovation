using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject settingsButton;
  //  [SerializeField] GameObject closeButton;
    [SerializeField] LockThing lockThing;

    bool shouldClose = false;

    void Start()
    {
        settingsButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(aaaa);
        //closeButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(CloseSettingsMenu);
    }

    void aaaa()
    {
        if (!shouldClose)
        {
            settingsMenu.SetActive(true);
            lockThing.LockSwipe();
        }

        else
        {
            settingsMenu.SetActive(false);
            lockThing.UnlockSwipe();
        }

        shouldClose = !shouldClose;
    }
    //void OpenSettingsMenu()
    //{
    //    settingsMenu.SetActive(true);
    //}

    //void CloseSettingsMenu()
    //{
    //    settingsMenu.SetActive(false);
    //}
}
