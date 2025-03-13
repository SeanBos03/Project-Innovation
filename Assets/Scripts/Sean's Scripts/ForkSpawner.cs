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
    [SerializeField] int timesBeforeSpawnReset = 3;
    int currentSpawnTime = 0;

    private Renderer theRenderer;
    private List<Vector3> previousSpawnedForks = new List<Vector3>(); //list to track previous spawn positions

    bool isReady = false;

    void Update()
    {
        if (!isReady)
        {
            if (GameData.gameStarts)
            {
                theRenderer = GetComponent<Renderer>();
                InvokeRepeating("SpawnFork", 0f, cycleTime);
                isReady = true;
            }
        }
    }

    void SpawnFork()
    {
        if (currentSpawnTime >= timesBeforeSpawnReset)
        {
            previousSpawnedForks.Clear();
            currentSpawnTime = 0;
        }

        for (int i = 0; i < amountOfForkPerCycle; i++)
        {
            Vector3 randomPoint;
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
            currentSpawnTime++;
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
