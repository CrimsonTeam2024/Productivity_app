using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CellController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOccupied = false;
    public GameObject buildingPrefab;

    public int gridX;
    public int gridY;

    [Header("Highlight Settings")]
    public Color HlCol = Color.green;
    private Color baseCol;

    private Image cellImage;

    void Awake()
    {
        // (Cell's Image component)
        cellImage = GetComponent<Image>();
        if (cellImage != null)
        {
            baseCol = cellImage.color;
        }
        else
        {
            Debug.LogWarning("CellController --> No Image compoenent on Cell prefab?");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cellImage != null)
        {
            cellImage.color = HlCol;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cellImage != null)
        {
            cellImage.color = baseCol;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Cell coord: {name}");

        // Ignore if occupied by bulding
        if (isOccupied)
        {
            Debug.Log(" - Cell occupied");
            BuildingStats stats = GetComponentInChildren<BuildingStats>();
            if (stats != null)
            {
                BuildingInfoPanel.Instance.Show(stats);
            }
            else
            {
                Debug.Log(" - Cell occupied. BuildingStats not found.");
            }
            return;
        }

        // Check adjacent to another building
        bool adjacent = VillageGridManagerRef().HasAdjacentBuilding(gridX, gridY);
        if (!adjacent)
        {
            Debug.Log(" - Can't build here - no adjacent building.");
            return;
        }

        BuildingSelectionPanel.Instance.ShowForCell(this);
        // \/ This \/ now bypassed by BuildingSelectionPanel,
        // which will then handle actual building:

        // Attempt ro spend coins / build, if adjacent check is true
        //if (!GameManager.Instance.SpendCoins(100))
        //{
        //    Debug.Log(" - Not enough coins");
        //    return;
        //}

        //// Checks good - place the building
        //isOccupied = true;
        //GameObject building = Instantiate(buildingPrefab, transform);
        //var buildingRect = building.GetComponent<RectTransform>();
        //if (buildingRect != null)
        //    buildingRect.anchoredPosition = Vector2.zero;

        //Debug.Log(" - Building placed");

    }

    private VillageGridManager VillageGridManagerRef()
    {
        return FindObjectOfType<VillageGridManager>();
    }
}
