using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkObject : MonoBehaviour
{
    [SerializeField]  float timeTillDisppear;
    public int amountOfDamage;
    void Start()
    {
        Invoke("DestroySelf", timeTillDisppear);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    public int GetDamage()
    {
        return amountOfDamage;
    }
}
