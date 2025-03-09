using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLoader : MonoBehaviour
{
    // Load a scene by its exact name
    public void LoadSpecificScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is empty or null! Please enter a valid scene name.");
        }
    }

    // Quit the application (useful for an Exit button)
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();

        // If running in the Unity Editor, stop playing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
