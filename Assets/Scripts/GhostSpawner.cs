using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefab; // Assign this in the Inspector
    public float initialSpawnRate = 5f; // How often to initially spawn a ghost
    public float spawnRateDecay = 0.5f; // How much to decrease spawn rate every 10 seconds
    public float minimumSpawnRate = 1f; // Minimum spawn rate to ensure it doesn't get too fast
    public float spawnDistance = 10f; // Distance from the player to spawn ghosts
    private float nextSpawnTime;
    private float currentSpawnRate;
    private float nextDecayTime = 10f; // Time until the next spawn rate decay

    // Reference to the player's transform
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentSpawnRate = initialSpawnRate;
        nextSpawnTime = Time.time + currentSpawnRate;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnGhost();
            nextSpawnTime = Time.time + currentSpawnRate;
        }

        if (Time.time >= nextDecayTime)
        {
            AdjustSpawnRate();
            nextDecayTime += 10f; // Set the next decay time
        }
    }

    void SpawnGhost()
    {
        Vector2 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 spawnPoint = playerTransform.position + new Vector3(spawnDirection.x, spawnDirection.y, 0);
        Instantiate(ghostPrefab, spawnPoint, Quaternion.identity);
    }

    void AdjustSpawnRate()
    {
        // Decrease the spawn rate to increase difficulty, but don't let it go below minimum
        currentSpawnRate = Mathf.Max(currentSpawnRate - spawnRateDecay, minimumSpawnRate);
        Debug.Log("Adjusted spawn rate: " + currentSpawnRate); // Optional: for debugging
    }
}
