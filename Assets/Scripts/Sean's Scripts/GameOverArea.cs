using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverArea : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameData.inTurortrial = false;
            GameData.TurtorialStage = 0;
        }
    }
}