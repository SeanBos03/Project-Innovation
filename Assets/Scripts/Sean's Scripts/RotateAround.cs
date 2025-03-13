using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 2f;
    [SerializeField] bool clockwise = true;

    void Update()
    {
        float direction = 1f;
        if (clockwise)
        {
        }
        else
        {
            direction = -1f;
        }
        transform.RotateAround(target.position, Vector3.up, direction * speed * Time.deltaTime);
    }
}