using UnityEngine;

public class WorldMapGenerator : MonoBehaviour
{
    public WorldGrid GridData;
    public GameObject GridCellPrefab; // Prefab for grid cell objects
    public Transform MapParent; // Parent object for cells

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse click position to grid position
            Vector2Int gridPosition = GetGridPositionFromMouse();
            if (IsValidGridPosition(gridPosition))
            {
                // Update the cell type in the grid
                GridData.UpdateCellType(gridPosition, 2); // Change to type '2' (example: building)

                // Regenerate the map to reflect changes
                GenerateMap();
            }
        }
    }

    private Vector2Int GetGridPositionFromMouse()
    {
        // Convert mouse position to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Convert world position to grid position (adjust based on cell size)
        int x = Mathf.FloorToInt(worldPosition.x);
        int y = Mathf.FloorToInt(worldPosition.y);
        return new Vector2Int(x, y);
    }

    private bool IsValidGridPosition(Vector2Int position)
    {
        return position.x >= 0 && position.x < GridData.Rows &&
               position.y >= 0 && position.y < GridData.Columns;
    }

    public void GenerateMap()
    {
        // Clear existing map objects
        foreach (Transform child in MapParent)
        {
            Destroy(child.gameObject);
        }

        // Generate new map based on grid data
        for (int row = 0; row < GridData.Rows; row++)
        {
            for (int col = 0; col < GridData.Columns; col++)
            {
                WorldGridCell cell = GridData.Cells[row, col];
                GameObject cellObject = Instantiate(GridCellPrefab, MapParent);

                // Set position and sprite
                cellObject.transform.localPosition = new Vector3(col, -row, 0);
                var spriteRenderer = cellObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = cell.CellSprite;
            }
        }
    }
}
