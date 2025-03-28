using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameManager theGameManager;
    bool isReady = false;
    // Update is called once per frame
    void Update()
    {
        if (!isReady)
        {
            if (GameData.rotationReady)
            {
                isReady = true;
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fork"))
        {
            GameData.life -= other.gameObject.GetComponent<ForkObject>().amountOfDamage;
            Destroy(other.gameObject);
        }

        CheckHealth();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
        {
            if (GameData.life != GameData.lifeMax)
            {
                GameData.life += other.gameObject.GetComponent<HealhItem>().GetHealth();
                Destroy(other.gameObject);
            }
        }

        CheckHealth();
    }

    void CheckHealth()
    {
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
