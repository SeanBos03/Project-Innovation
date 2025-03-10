using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] float fallStrength = 1f;
    [SerializeField] float disappearDelay = 1f; //time before plate disappears
    bool hasStarted;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !hasStarted)
        {
            StartCoroutine(DisappearAfterDelay());
            InvokeRepeating("ObjectFall", 1f, 1f);
            hasStarted = true;
        }
    }

    IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay);
        Debug.Log("Pressure plate destroyed!");
        Destroy(gameObject);
    }

    void ObjectFall()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - fallStrength, transform.position.z);
    }
}


