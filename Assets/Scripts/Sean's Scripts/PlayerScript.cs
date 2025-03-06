using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameManager theGameManager;
    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fork"))
        {
            GameData.life -= other.gameObject.GetComponent<ForkObject>().amountOfDamage;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Health"))
        {
            GameData.life += other.gameObject.GetComponent<HealhItem>().GetHealth();
            Destroy(other.gameObject);
        }

        if (GameData.life <= 0)
        {
            GameData.life = GameData.lifeMax;
            theGameManager.GetComponent<SceneReload>().RestartScene();
            
        }

        if (GameData.life > GameData.lifeMax)
        {
            GameData.life = GameData.lifeMax;
        }
    }
}
