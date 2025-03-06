using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class ForkSpawner : MonoBehaviour
{
    [SerializeField] Vector3 forkSpawnRotation = Vector3.zero;
    [SerializeField] GameObject theForkObject;
    [SerializeField] float heightForkFallFromSurface;
    [SerializeField] float cycleTime;
    [SerializeField] int amountOfForkPerCycle;
    Renderer theRenderer;

    void Start()
    {
        theRenderer = GetComponent<Renderer>();
       // SpawnFork();
        InvokeRepeating("SpawnFork", 0f, cycleTime);
    }
    void Update()
    {
    }

    void SpawnFork()
    {
        for (int i = 0; i < amountOfForkPerCycle; i++)
        {
            float randomX = UnityEngine.Random.Range(theRenderer.bounds.center.x - theRenderer.bounds.size.x / 2, theRenderer.bounds.center.x + theRenderer.bounds.size.x / 2);
            float randomZ = UnityEngine.Random.Range(theRenderer.bounds.center.z - theRenderer.bounds.size.z / 2, theRenderer.bounds.center.z + theRenderer.bounds.size.z / 2);
            Vector3 randomPoint = new Vector3(randomX, theRenderer.bounds.center.y + heightForkFallFromSurface, randomZ);
            Instantiate(theForkObject, randomPoint, Quaternion.Euler(forkSpawnRotation));
        }
    }
}
