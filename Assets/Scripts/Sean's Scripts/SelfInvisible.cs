using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfInvisible : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
