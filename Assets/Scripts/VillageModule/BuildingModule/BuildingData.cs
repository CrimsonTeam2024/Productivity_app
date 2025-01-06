using UnityEngine;

public class BuildingStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealthDisplay = 100;
    public float currentHealth = 100;
    public float decaySpeed = 0.1f;
    VillageController villageController;

    void Awake()
    {
        currentHealth = maxHealth;
        villageController = FindFirstObjectByType<VillageController>();
    }

    void Update()
    {
        decaySpeed = villageController.villageDecayRate;

        if (currentHealth > 0)
        {
            currentHealth -= decaySpeed * Time.deltaTime;
        }

        currentHealthDisplay = (int)currentHealth;
    }
}
