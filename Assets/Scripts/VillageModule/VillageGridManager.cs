using UnityEngine;
using UnityEngine.UI;

public class VillageGridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridWidth = 5;
    public int gridHeight = 5;

    // Cell overlap is bad
    // eg. Cell prefab size 100x100, so cellSizeX=100, cellSizeY=100.
    public float cellSizeX = 100f;
    public float cellSizeY = 100f;

    public GameObject cellPrefab; // CellController script!

    private CellController[,] cellArray;

    void Start()
    {
        GenerateGrid();
        PlaceDefaultBuildingInCenter();
    }

    private void GenerateGrid()
    {
        cellArray = new CellController[gridWidth, gridHeight];

        // grid dimensions
        float totalWidth = (gridWidth - 1) * cellSizeX;
        float totalHeight = (gridHeight - 1) * cellSizeY;

        // Half offsets for centering the entire grid
        float halfWidth = totalWidth * 0.5f;
        float halfHeight = totalHeight * 0.5f;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject cellObj = Instantiate(cellPrefab, transform);
                cellObj.name = $"Cell ({x},{y})";

                RectTransform rect = cellObj.GetComponent<RectTransform>();
                // Anchor Cell arrangement on parent (0,0) position (eg. Village, which is clipped/masked)
                float posX = x * cellSizeX - halfWidth;
                float posY = -(y * cellSizeY - halfHeight);

                rect.anchoredPosition = new Vector2(posX, posY);

                // 2D array of Cell refs
                CellController cc = cellObj.GetComponent<CellController>();
                cc.gridX = x;
                cc.gridY = y;

                cellArray[x, y] = cc;
            }
        }
    }

    // Default initial building prefab in center of grid 
    // ie. Some buildable area is given at the start

    private void PlaceDefaultBuildingInCenter()
    {
        int centerX = gridWidth / 2;
        int centerY = gridHeight / 2;

        var centerCell = cellArray[centerX, centerY];
        if (centerCell == null) return;

        centerCell.isOccupied = true;
        if (centerCell.buildingPrefab != null)
        {
            GameObject building = Instantiate(centerCell.buildingPrefab, centerCell.transform);
            RectTransform br = building.GetComponent<RectTransform>();
            if (br != null) br.anchoredPosition = Vector2.zero;
        }
    }

    // Check position for occupied cells adjacent including diagonally,
    // returns true if at least one adjacent "occupied" by building.

    public bool HasAdjacentBuilding(int x, int y)
    {
        // Check adjacent Cells (ie. -1 to 1) along x and y
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                    continue;

                int nx = x + dx;
                int ny = y + dy;

                // Edge
                if (nx >= 0 && nx < gridWidth && ny >= 0 && ny < gridHeight)
                {
                    if (cellArray[nx, ny].isOccupied)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
