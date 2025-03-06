using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
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
    }
}
