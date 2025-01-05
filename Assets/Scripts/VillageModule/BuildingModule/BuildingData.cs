using UnityEngine;

public class BuildingStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;

    void Awake()
    {
        currentHealth = maxHealth;
    }
}
