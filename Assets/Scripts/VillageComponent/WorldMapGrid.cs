using System.Collections.Generic;
using UnityEngine;

public class WorldGrid
{
    public int Rows { get; private set; }
    public int Columns { get; private set; }
    public WorldGridCell[,] Cells { get; private set; }

    public WorldGrid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        Cells = new WorldGridCell[rows, columns];

        // init grid with default empty cells
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Cells[row, col] = new WorldGridCell(new Vector2Int(row, col), 0, 0); // Default terrain and type
            }
        }
    }

    public void UpdateCellType(Vector2Int position, int newType)
    {
        var cell = Cells[position.x, position.y];
        cell.Type = newType;
        cell.UpdateSprite(); // update sprite per new state
    }
}
