using UnityEngine;

//for achieving the thrid person effect
public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    Vector3 initialOffset;

    void Start()
    {
        initialOffset = transform.position - player.transform.position;
    }
    void LateUpdate()
    {
        transform.position = player.transform.position + initialOffset;
    }
}