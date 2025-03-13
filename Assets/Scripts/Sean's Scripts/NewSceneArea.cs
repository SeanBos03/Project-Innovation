using UnityEngine;
using UnityEngine.SceneManagement;

public class NewSceneArea : MonoBehaviour
{
    [SerializeField] string theName;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(theName);
            GameData.inTurortrial = false;
            GameData.TurtorialStage = 0;
        }
    }
}