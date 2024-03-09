using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float initialSpawnRate = 5f; // Initial spawn rate for zombies
    public float spawnRateDecay = 0.5f; // Decrease spawn rate by this amount every 10 seconds
    public float minimumSpawnRate = 1f; // Minimum spawn rate
    public float spawnDistance = 10f; // Distance from the player to spawn zombies
    private float nextSpawnTime;
    private float currentSpawnRate;
    private float nextDecayTime;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentSpawnRate = initialSpawnRate;
        nextSpawnTime = Time.time + currentSpawnRate;
        nextDecayTime = Time.time + 10f; // Set the first decay time 10 seconds from now
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnZombie();
            nextSpawnTime = Time.time + currentSpawnRate;
        }

        if (Time.time >= nextDecayTime)
        {
            AdjustSpawnRate();
            nextDecayTime = Time.time + 10f; // Schedule the next decay
        }
    }

    void SpawnZombie()
    {
        Vector2 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 spawnPoint = playerTransform.position + new Vector3(spawnDirection.x, spawnDirection.y, 0);
        Instantiate(zombiePrefab, spawnPoint, Quaternion.identity);
    }

    void AdjustSpawnRate()
    {
        currentSpawnRate = Mathf.Max(currentSpawnRate - spawnRateDecay, minimumSpawnRate);
        Debug.Log("Adjusted spawn rate: " + currentSpawnRate); // For debugging
    }
}
