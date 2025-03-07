using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject cam2;
    [SerializeField] GameObject cam3;
    public void CamToggle()
    {
        if (GameData.cam == 1)
        {
            GameData.cam = 2;
            cam1.SetActive(false);
            cam3.SetActive(false);
            cam2.SetActive(true);
            return;
        }

        if (GameData.cam == 2)
        {
            GameData.cam = 3;
            cam3.SetActive(true);
            cam2.SetActive(false);
            return; 
        }

        if (GameData.cam == 3)
        {
            GameData.cam = 1;
            cam1.SetActive(true);
            cam3.SetActive(false);
            return;
        }
    }
}
