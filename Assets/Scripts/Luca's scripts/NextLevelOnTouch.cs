using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelOnTouch : MonoBehaviour
{
    [SerializeField] string sceneName = "SceneNameHere";
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    SceneManager.LoadScene(sceneName);
        //}

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
