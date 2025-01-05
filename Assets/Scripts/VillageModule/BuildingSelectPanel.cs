using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectionPanel : MonoBehaviour
{
    public static BuildingSelectionPanel Instance;

    [Header("Building Prefabs")]
    public GameObject buildingPrefabA;
    public uint costA;
    public GameObject buildingPrefabB;
    public uint costB;
    public GameObject buildingPrefabC;
    public uint costC;

    private CellController currentCell;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void ShowForCell(CellController cell)
    {
        currentCell = cell;
        gameObject.SetActive(true);
    }

    public void OnClickBuildA()
    {
        AttemptBuild(buildingPrefabA, costA);
    }

    public void OnClickBuildB()
    {
        AttemptBuild(buildingPrefabB, costB);
    }

    public void OnClickBuildC()
    {
        AttemptBuild(buildingPrefabC, costC);
    }

    private void AttemptBuild(GameObject buildingPrefab, uint cost)
    {
        Debug.Log("Build attempted...");

        // Validate Cell (eg. occupied, adjacency)
        if (currentCell.isOccupied)
        {
            Debug.Log("Cell occupied");
            return;
        }

        // Check sufficient coins
        if (!GameManager.Instance.SpendCoins(cost))
        {
            Debug.Log("Insufficient coins");
            return;
        }

        // Place building
        currentCell.isOccupied = true;
        GameObject building = Instantiate(buildingPrefab, currentCell.transform);
        var buildingRect = building.GetComponent<RectTransform>();
        if (buildingRect != null)
            buildingRect.anchoredPosition = Vector2.zero;

        Debug.Log("Building placed");

        // Then hide the panel
        gameObject.SetActive(false);
    }

    public void OnClickCancel()
    {
        gameObject.SetActive(false);
    }
}
