using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkSpawner : MonoBehaviour
{
    [SerializeField] Vector3 forkSpawnRotation = Vector3.zero;
    [SerializeField] GameObject theForkObject;
    [SerializeField] float heightForkFallFromSurface;
    [SerializeField] float cycleTime;
    [SerializeField] int amountOfForkPerCycle;
    [SerializeField] float minSpawnDistance = 2.0f; //minimum distance between forks

    private Renderer theRenderer;
    private List<Vector3> previousSpawnedForks = new List<Vector3>(); //list to track previous spawn positions

    void Start()
    {
        theRenderer = GetComponent<Renderer>();
        InvokeRepeating("SpawnFork", 0f, cycleTime);
    }

    void SpawnFork()
    {
        for (int i = 0; i < amountOfForkPerCycle; i++)
        {
            Vector3 randomPoint;
            previousSpawnedForks.Clear();
            int maxAttempts = 15; //max amount of times to try to get a valid spawn
            int attempts = 0;

            do
            {
                float randomX = UnityEngine.Random.Range(
                    theRenderer.bounds.center.x - theRenderer.bounds.size.x / 2,
                    theRenderer.bounds.center.x + theRenderer.bounds.size.x / 2
                );
                float randomZ = UnityEngine.Random.Range(
                    theRenderer.bounds.center.z - theRenderer.bounds.size.z / 2,
                    theRenderer.bounds.center.z + theRenderer.bounds.size.z / 2
                );

                randomPoint = new Vector3(randomX, theRenderer.bounds.center.y + heightForkFallFromSurface, randomZ);
                attempts++;

                if (attempts >= maxAttempts) //stop if too many attempts are made
                {
                    Debug.Log("too many attempts to spawn");
                    break;
                }

            } while (IsTooClose(randomPoint));

            float theY = Random.Range(0, 360);
            forkSpawnRotation.y = theY;
            previousSpawnedForks.Add(randomPoint); //store the new valid spawn
            Instantiate(theForkObject, randomPoint, Quaternion.Euler(forkSpawnRotation)); //spawn the fork
        }
    }

    bool IsTooClose(Vector3 newPosition)
    {
        foreach (Vector3 spawnedPosition in previousSpawnedForks)
        {
            if (Vector3.Distance(newPosition, spawnedPosition) < minSpawnDistance)
            {
                return true;
            }
        }
        return false; //mean a valid spawn
    }
}
