using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelOnTouch : MonoBehaviour
{
    // Option 1: Specify the scene by its name
    public string sceneName = "SceneNameHere"; // Replace with the name of your scene

    // Option 2: Alternatively, specify the scene by index
    // public int sceneIndex = 1; // For example, if you want to load scene with index 1

    void Update()
    {
        // Check for touch input (or mouse click on desktop)
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse click detected!");
            LoadSpecificScene();
        }
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("Touch detected!");
            LoadSpecificScene();
        }
#endif
    }

    void LoadSpecificScene()
    {
        // Option 1: Load the scene by name
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log("Loading scene: " + sceneName);
        }

        // Option 2: Load the scene by index (uncomment to use this method instead)
        // else if (sceneIndex >= 0)
        // {
        //     SceneManager.LoadScene(sceneIndex);
        //     Debug.Log("Loading scene at index: " + sceneIndex);
        // }
    }
}
