using UnityEngine;

public class BuildingOracle : MonoBehaviour
{
    [SerializeField] private GameObject[] treeImages;

    private VillageController villageController;

    void Start()
    {
        villageController = FindFirstObjectByType<VillageController>();
    }

    public void Update()
    {
        if (villageController == null)
            return;

        // Otherwise, fraction-based
        float fraction = villageController.villageHealth;
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