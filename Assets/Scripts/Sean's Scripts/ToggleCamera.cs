using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject cam2;
    public void CamToggle()
    {
        if (GameData.cam == 1)
        {
            GameData.cam = 2;
            cam1.SetActive(false);
            cam2.SetActive(true);
            return;
        }

        if (GameData.cam == 2)
        {
            GameData.cam = 1;
            cam1.SetActive(true);
            cam2.SetActive(false);
            return; 
        }
    }
}
