using UnityEngine;

public class BuildingOracle : MonoBehaviour
{
    [SerializeField] private GameObject[] treeImages;

    private VillageController villageController;

    void Start()
    {
        villageController = FindObjectOfType<VillageController>();
    }

    public void Update()
    {
        if (villageController == null)
            return;

        float total = villageController.totalVillageHealth;
        float health = villageController.villageHealth;
        // Avoid divide-by-zero for (totalVillageHealth == 0)

        if (total <= 0f)
        {
            // Default image
            ShowTreeAtIndex(4);
            return;
        }

        // Otherwise, fraction-based
        float fraction = health / total;
        fraction = Mathf.Clamp01(fraction);

        int index = Mathf.FloorToInt(fraction * 5f);
        if (index > 4) index = 4;

        ShowTreeAtIndex(index);
    }

    private void ShowTreeAtIndex(int index)
    {
        for (int i = 0; i < treeImages.Length; i++)
        {
            treeImages[i].SetActive(i == index);
        }
    }
}