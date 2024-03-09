using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    public GameObject batPrefab;
    public float initialSpawnInterval = 2f; // Initial interval between bat spawns
    public float spawnIntervalDecay = 0.1f; // Amount to decrease the spawn interval every 10 seconds
    public float minimumSpawnInterval = 0.5f; // Minimum spawn interval to prevent too frequent spawns
    public float spawnRadius = 10f; // The radius around the player within which bats can spawn

    private float timeSinceLastSpawn;
    private float currentSpawnInterval;
    private float timeSinceLastDecay; // Track time since last spawn interval decay
    private Transform playerTransform;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        currentSpawnInterval = initialSpawnInterval;
        timeSinceLastDecay = 0f;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        timeSinceLastSpawn += Time.deltaTime;
        timeSinceLastDecay += Time.deltaTime;

        if (timeSinceLastSpawn >= currentSpawnInterval)
        {
            SpawnBatAroundPlayer();
            timeSinceLastSpawn = 0;
        }

        // Decrease spawn interval every 10 seconds
        if (timeSinceLastDecay >= 10f)
        {
            currentSpawnInterval = Mathf.Max(currentSpawnInterval - spawnIntervalDecay, minimumSpawnInterval);
            timeSinceLastDecay = 0f; // Reset decay timer
        }
    }

    void SpawnBatAroundPlayer()
    {
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPosition = (Vector2)playerTransform.position + spawnDirection * spawnRadius;
        Instantiate(batPrefab, spawnPosition, Quaternion.identity);
    }
}
