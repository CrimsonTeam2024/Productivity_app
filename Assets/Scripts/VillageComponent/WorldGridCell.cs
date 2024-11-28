using UnityEngine;
using UnityEngine.U2D;

public class WorldGridCell
{
    public Vector2Int Position { get; private set; }
    public int Terrain { get; set; }
    public int Type { get; set; }
    public int UpgradeLevel { get; set; }
    public float HealthLevel { get; set; }
    public float RadiusBonuses { get; set; }
    public Sprite CellSprite { get; set; }

    public WorldGridCell(Vector2Int position, int terrain, int type)
    {
        Position = position;
        Terrain = terrain;  
        Type = type;
        UpgradeLevel = 0;
        HealthLevel = 100f;
        RadiusBonuses = 0f;
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        if (Type == 0)
            CellSprite = SpriteManager.GetSprite("Empty");
        else if (Type == 1)
            CellSprite = SpriteManager.GetSprite("Buildable");
        else if (Type >= 2 && Type <= 4)
            CellSprite = SpriteManager.GetSprite($"Building_Level{Type}_Upgrade{UpgradeLevel}");
        else
            CellSprite = SpriteManager.GetSprite("Unknown");
    }
}
