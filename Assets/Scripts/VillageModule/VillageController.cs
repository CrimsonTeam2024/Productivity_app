using UnityEngine;

public class VillageController : MonoBehaviour
{
    public float villageDecayRate = 1f;   
    public float totalVillageHealth = 1f;
    public float villageHealth;
    public int buildingCounter = 0;
    public float maxVillageHealth = 1f;

    [SerializeField] Oracle oracle;

    void Start()
    {
        maxVillageHealth = 1f;
        totalVillageHealth = 1f;        
    }

    void LateUpdate()
    {
        if (maxVillageHealth == 0)
        {
            villageHealth = 1f;
        }
        else
        {
            villageHealth = totalVillageHealth / maxVillageHealth;
            totalVillageHealth = 0f;
            maxVillageHealth = 0f;
        }

        oracle.oracleSatisfaction = villageHealth;
    }
}