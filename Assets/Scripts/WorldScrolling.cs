using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject tilePrefab;

    private Vector2Int playerTilePosition = new Vector2Int(0, 0);
    // Initialize to a different value to ensure update on start
    private Vector2Int previousPlayerTilePosition = new Vector2Int(-1, -1); 

    public int loadRadius = 2; // How many tiles around the player to keep loaded
    public float tileSize = 20f; // The size of each tile

    // Dictionary to keep track of loaded tiles
    private Dictionary<Vector2Int, GameObject> loadedTiles = new Dictionary<Vector2Int, GameObject>();

    private void Update()
    {
        playerTilePosition.x = Mathf.FloorToInt(playerTransform.position.x / tileSize);
        playerTilePosition.y = Mathf.FloorToInt(playerTransform.position.y / tileSize);

        if (playerTilePosition != previousPlayerTilePosition)
        {
            UpdateLoadedTiles();
            previousPlayerTilePosition = playerTilePosition;
        }
    }

    private void UpdateLoadedTiles()
    {
        HashSet<Vector2Int> tilesToKeep = new HashSet<Vector2Int>();

        // Determine which tiles should be loaded
        for (int x = playerTilePosition.x - loadRadius; x <= playerTilePosition.x + loadRadius; x++)
        {
            for (int y = playerTilePosition.y - loadRadius; y <= playerTilePosition.y + loadRadius; y++)
            {
                Vector2Int tilePos = new Vector2Int(x, y);
                if (!loadedTiles.ContainsKey(tilePos))
                {
                    // Load new tile
                    Vector3 worldPosition = new Vector3(x * tileSize, y * tileSize, 0);
                    GameObject tile = Instantiate(tilePrefab, worldPosition, Quaternion.identity);
                    tile.SetActive(true); // Ensure the instantiated tile is active
                    loadedTiles.Add(tilePos, tile);
                }
                tilesToKeep.Add(tilePos);
            }
        }

        // Determine which tiles should be unloaded
        List<Vector2Int> tilesToUnload = new List<Vector2Int>();
        foreach (var tile in loadedTiles)
        {
            if (!tilesToKeep.Contains(tile.Key))
            {
                tilesToUnload.Add(tile.Key);
            }
        }

        // Unload tiles
        foreach (var tilePos in tilesToUnload)
        {
            Destroy(loadedTiles[tilePos]);
            loadedTiles.Remove(tilePos);
        }
    }
}
