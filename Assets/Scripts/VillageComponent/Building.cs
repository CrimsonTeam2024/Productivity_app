using UnityEngine;

public class Building : MonoBehaviour
{
    public int health = 100;
    public int upgradeLevel = 1;
    public int maxHealth = 100;

    void Start()
    {
        InvokeRepeating("DecreaseHealth", 60f, 60f); // Decrease health, eg. every 60 seconds
    }

    void DecreaseHealth()
    {
        health -= upgradeLevel * 2; // Deterioration rate
        if (health <= 0)
        {
            DestroyBuilding();
        }
    }

    public void Repair(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }

    void DestroyBuilding()
    {
        // Remove building's position from occupiedPositions
        // etc?
        BuildingManager buildingManager = FindObjectOfType<BuildingManager>();
        if (buildingManager != null)
        {
            Vector2Int gridPosition = buildingManager.GridPositionFromWorld(transform.position);
            buildingManager.RemoveBuildingAt(gridPosition);
        }
        Destroy(gameObject);
    }
}
