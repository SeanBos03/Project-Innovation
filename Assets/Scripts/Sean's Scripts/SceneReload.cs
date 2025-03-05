using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReload : MonoBehaviour
{
    public void RestartScene()
    {
        GameData.cam = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}