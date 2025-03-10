using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReload : MonoBehaviour
{
    public void RestartScene()
    {
        GameData.cam = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToScene2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void ToScene1()
    {
        SceneManager.LoadScene("Level1");
    }
}