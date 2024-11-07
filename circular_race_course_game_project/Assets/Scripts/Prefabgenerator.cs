using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PrefabGenerator : MonoBehaviour
{
    public GameObject eggplantPrefab;
    public GameObject coinPrefab;
    public GameObject oilPuddlePrefab;
    public Tilemap roadTilemap;

    private ObjectPlacementManager objectPlacementManager;
    private HashSet<Vector3Int> occupiedCells;
    private ReadConfigFile noOfOilSpills;

    public bool spawnOilPuddle;

    // Start is called before the first frame update
    void Start()
    {
        // Generate objects when the script starts
        GenerateObjects();
        // Invoke GenerateObjects after 1 second (commented out)
        // Invoke("GenerateObjects", 1f);
        
        // Find the ReadConfigFile instance in the scene
        noOfOilSpills = FindObjectOfType<ReadConfigFile>();
    }

    // GenerateObjects method called at the start and can be called elsewhere
    public void GenerateObjects()
    {
        // Find the ObjectPlacementManager instance in the scene
        objectPlacementManager = FindObjectOfType<ObjectPlacementManager>();
        
        // Initialize the set of occupied cells
        occupiedCells = new HashSet<Vector3Int>();
        
        // Generate prefabs based on server response counts
        GeneratePrefabs(coinPrefab, objectPlacementManager.coinsFromServer);
        GeneratePrefabs(eggplantPrefab, objectPlacementManager.eggplantsFromServer);
        
        // Check if oil puddles should be spawned based on a boolean flag
        if (spawnOilPuddle)
        {
            GeneratePrefabs(oilPuddlePrefab, ReadConfigFile.oilSpills);
            spawnOilPuddle = false;
        }
    }

    // GeneratePrefabs method for spawning prefabs in random positions
    public void GeneratePrefabs(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Get a random cell and convert it to world coordinates
            Vector3Int randomCell = GetRandomCell();
            Vector3 spawnPosition = roadTilemap.GetCellCenterWorld(randomCell);

            // Check if the cell is already occupied or has a specific collider tag
            while (occupiedCells.Contains(randomCell) || CellHasColliderWithTag(randomCell, "StartLine"))
            {
                randomCell = GetRandomCell();
                spawnPosition = roadTilemap.GetCellCenterWorld(randomCell);
            }

            // Mark the cell as occupied
            occupiedCells.Add(randomCell);
            
            // Instantiate the prefab at the calculated position
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }

    // Check if a cell has a collider with a specific tag
    public bool CellHasColliderWithTag(Vector3Int cell, string tag)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(roadTilemap.GetCellCenterWorld(cell));
    
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(tag))
            {
                return true;
            }
        }

        return false;
    }

    // Get a random cell within the bounds of the road tilemap
    public Vector3Int GetRandomCell()
    {
        BoundsInt bounds = roadTilemap.cellBounds;
        Vector3Int randomCell;

        do
        {
            randomCell = new Vector3Int(
                Random.Range(bounds.x, bounds.x + bounds.size.x), //Width of the bounding box
                Random.Range(bounds.y, bounds.y + bounds.size.y), //height of the bounding box
                0
            );
        } while (!IsCellOnTrack(randomCell));

        return randomCell;
    }

    // Check if a cell is on the track based on the road tilemap
    public bool IsCellOnTrack(Vector3Int cell)
    {
        TileBase tile = roadTilemap.GetTile(cell);
        // Add any additional checks here based on your tilemap setup
        return tile != null; //if there's a tile it returns true otherwise false
    }

    // Check if a position is within the boundaries of the track
    public bool IsPositionWithinTrackBoundaries(Vector3 position)
    {
        Tilemap trackTilemap = roadTilemap; 

        BoundsInt cellBounds = trackTilemap.cellBounds;

        Vector3 minWorldBounds = trackTilemap.GetCellCenterWorld(cellBounds.min); //minimum X, Y, and Z coordinates of the bounding box.
        Vector3 maxWorldBounds = trackTilemap.GetCellCenterWorld(cellBounds.max); //maximum X, Y and Z coordinates of the bounding box

        return position.x >= minWorldBounds.x && position.x <= maxWorldBounds.x &&
               position.y >= minWorldBounds.y && position.y <= maxWorldBounds.y;
    }
}
