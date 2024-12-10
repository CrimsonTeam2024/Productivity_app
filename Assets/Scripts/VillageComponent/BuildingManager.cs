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

    Vector3 GetGridCenter()
    // Find center pos of Terrain Grid GameObject
    {
        BoundsInt cellBounds = terrainTilemap.cellBounds;

        Vector3Int centerCellPosition = new Vector3Int(
            cellBounds.xMin + cellBounds.size.x / 2,
            cellBounds.yMin + cellBounds.size.y / 2,
            0
        );

        Vector3 worldCenterPosition = terrainTilemap.CellToWorld(centerCellPosition);

        Vector3 cellCenterOffset = new Vector3(terrainTilemap.cellSize.x / 2, terrainTilemap.cellSize.y / 2, 0);
        worldCenterPosition += cellCenterOffset;

        return worldCenterPosition;
    }


    void PlaceInitialBuilding()
    {
        Vector3 centerPosition = GetGridCenter();
        // get placement positions relative to terrain grid
        Vector3Int cellPosition = terrainTilemap.WorldToCell(centerPosition);
        Vector3 alignedPosition = terrainTilemap.CellToWorld(cellPosition);

        alignedPosition.z = -0.1f; // for above-grid layering
        // TODO: ensure later that this scales with (x,y)++

        GameObject initialBuildingInstance = Instantiate(initialBuildingPrefab, alignedPosition, Quaternion.identity, transform);
        initialBuildingInstance.transform.localScale = Vector3.one * 25; // some unclear scaling issues...
        initialBuildingInstance.SetActive(true);
        occupiedPositions.Add(new Vector2Int(cellPosition.x, cellPosition.y));
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
            Debug.Log("Cannot build here.");
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
