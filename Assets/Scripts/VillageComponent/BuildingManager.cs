using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    public GameObject initialBuildingPrefab;
    public GameObject buildingPrefab;
    public Tilemap terrainTilemap;
    public uint buildingCost = 100;

    private HashSet<Vector2Int> occupiedPositions = new HashSet<Vector2Int>();

    void Start()
    {
        // Assign terrainTilemap (if not)
        if (terrainTilemap == null)
        {
            terrainTilemap = FindObjectOfType<Tilemap>();
        }

        PlaceInitialBuilding();
    }

    void PlaceInitialBuilding()
    {
        Vector3 centerPosition = Vector3.zero; // Adjust to center
        Vector2Int gridPosition = GridPositionFromWorld(centerPosition);

        Instantiate(initialBuildingPrefab, centerPosition, Quaternion.identity, transform);
        occupiedPositions.Add(gridPosition);
    }

    public Vector2Int GridPositionFromWorld(Vector3 worldPosition)
    {
        Vector3Int cellPosition = terrainTilemap.WorldToCell(worldPosition);
        return new Vector2Int(cellPosition.x, cellPosition.y);
    }

    public Vector3 WorldPositionFromGrid(Vector2Int gridPosition)
    {
        return terrainTilemap.CellToWorld(new Vector3Int(gridPosition.x, gridPosition.y, 0));
    }

    public bool CanBuildAt(Vector2Int gridPosition)
    {
        if (occupiedPositions.Contains(gridPosition))
            return false;

        Vector2Int[] directions = {
            Vector2Int.up, Vector2Int.down,
            Vector2Int.left, Vector2Int.right,
            new Vector2Int(1,1), new Vector2Int(-1,-1),
            new Vector2Int(1,-1), new Vector2Int(-1,1)
        };

        foreach (var dir in directions)
        {
            Vector2Int adjacentPos = gridPosition + dir;
            if (occupiedPositions.Contains(adjacentPos))
                return true;
        }
        return false;
    }

    public void TryPlaceBuildingAt(Vector2Int gridPosition)
    {
        if (CanBuildAt(gridPosition))
        {
            // Check if enough coins
            if (GameManager.Instance.coins >= buildingCost)
            {
                GameManager.Instance.coins -= buildingCost; // Deduct coins
                Vector3 worldPosition = WorldPositionFromGrid(gridPosition);
                Instantiate(buildingPrefab, worldPosition, Quaternion.identity, transform);
                occupiedPositions.Add(gridPosition);
                Debug.Log("Building placed at: " + gridPosition);
            }
            else
            {
                Debug.Log("Not enough coins to build.");
            }
        }
        else
        {
            Debug.Log("Cannot build here. Must be adjacent to an existing building.");
        }
    }

    public void RemoveBuildingAt(Vector2Int gridPosition)
    {
        if (occupiedPositions.Contains(gridPosition))
        {
            occupiedPositions.Remove(gridPosition);
            Debug.Log("Building removed at: " + gridPosition);
        }
    }
}
