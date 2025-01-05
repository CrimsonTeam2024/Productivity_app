using UnityEngine;
using UnityEngine.EventSystems;

public class CellController : MonoBehaviour, IPointerClickHandler
{
    public bool isOccupied = false;
    public GameObject buildingPrefab; // UI object

    public int gridX;
    public int gridY;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Cell coord: {name}");

        // Ignore if occupied by bulding
        if (isOccupied)
        {
            Debug.Log(" - Cell occupied");
            return;
        }

        // Check adjacent to another building
        bool adjacent = VillageGridManagerRef().HasAdjacentBuilding(gridX, gridY);
        if (!adjacent)
        {
            Debug.Log(" - Can't build here - no adjacent building.");
            return;
        }

        // Attempt ro spend coins / build, if adjacent check is true
        if (!GameManager.Instance.SpendCoins(100))
        {
            Debug.Log(" - Not enough coins");
            return;
        }

        // Checks good - place the building
        isOccupied = true;
        GameObject building = Instantiate(buildingPrefab, transform);
        var buildingRect = building.GetComponent<RectTransform>();
        if (buildingRect != null)
            buildingRect.anchoredPosition = Vector2.zero;

        Debug.Log(" - Building placed");
    }

    private VillageGridManager VillageGridManagerRef()
    {
        // TODO: just static property or FindObjectOfType?
        return FindObjectOfType<VillageGridManager>();
    }
}
