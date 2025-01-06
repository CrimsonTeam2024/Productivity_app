using UnityEngine;

public class VillageController : MonoBehaviour
{
    public float villageDecayRate = 1f;   
    public float totalVillageHealth = 1f;
    public float villageHealth;
    public int buildingCounter = 0;

    void LateUpdate()
    {
        villageHealth = totalVillageHealth / (buildingCounter*100f);
        totalVillageHealth = 0f;
    }
}