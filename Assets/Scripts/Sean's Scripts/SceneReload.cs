using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReload : MonoBehaviour
{
    public void RestartScene()
    {
        GameData.cam = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameData.inTurortrial = false;
        GameData.TurtorialStage = 0;
    }

    public void ToScene(string theSceneName)
    {
        SceneManager.LoadScene(theSceneName);
        GameData.inTurortrial = false;
        GameData.TurtorialStage = 0;
    }

    public void ToScene2()
    {
        SceneManager.LoadScene("Level2");
        GameData.inTurortrial = false;
        GameData.TurtorialStage = 0;
    }

    public void ToScene1()
    {
        SceneManager.LoadScene("Level1");
        GameData.inTurortrial = false;
        GameData.TurtorialStage = 0;
    }
}