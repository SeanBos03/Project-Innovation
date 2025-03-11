using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfInvisible : MonoBehaviour
{
    [SerializeField] bool disableRenderer = true;
    [SerializeField] bool disableCollider = true;
    void Start()
    {
        if (disableRenderer)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }

        if (disableCollider)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
