using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlateDisappear : MonoBehaviour
{
    [SerializeField] float timeBeforeplateFalls = 1f;
    [SerializeField] float fallStrength = 1f;
    [SerializeField] float disappearDelay = 1f; //time before plate disappears
    bool hasStarted;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !hasStarted)
        {
            Invoke("DisappearAfterDelay", disappearDelay);
            Invoke("ObjectFall", timeBeforeplateFalls);
            hasStarted = true;
        }
    }

    void DisappearAfterDelay()
    {
        Destroy(gameObject);
    }

    void ObjectFall()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - fallStrength, transform.position.z);
    }
}


