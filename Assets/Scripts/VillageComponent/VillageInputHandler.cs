using UnityEngine;
using UnityEngine.EventSystems;

public class VillageInputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // TODO: figure out IPointerClickHandler inheritence

    public Camera villageCamera;
    private bool isPointerDown = false;
    private float pointerDownTime;
    private Vector2 pointerDownPosition;
    private const float clickThreshold = 0.3f; // Time in seconds
    private const float dragThreshold = 10f;   // Movement in pixels

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        pointerDownTime = Time.time;
        pointerDownPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPointerDown)
        {
            isPointerDown = false;
            float elapsedTime = Time.time - pointerDownTime;
            float distance = Vector2.Distance(eventData.position, pointerDownPosition);

            if (elapsedTime < clickThreshold && distance < dragThreshold)
            {
                HandleClick(eventData);
            }
            // Otherwise it's a drag, let the ScrollRect handle it
        }
    }

    private void HandleClick(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Convert screen point to local point in the RectTransform
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint);

        // Adjust for the pivot
        Vector2 pivotAdjustedPoint = localPoint + rectTransform.rect.size * rectTransform.pivot;

        // Normalize coordinates (0 to 1)
        Vector2 normalizedPoint = new Vector2(
            pivotAdjustedPoint.x / rectTransform.rect.width,
            pivotAdjustedPoint.y / rectTransform.rect.height
        );

        // Convert normalized point to world point
        Vector3 worldPoint = villageCamera.ViewportToWorldPoint(new Vector3(normalizedPoint.x, normalizedPoint.y, villageCamera.nearClipPlane));

        // Cast a ray at the world point
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            HandleBuildingInteraction(hit);
        }
        else
        {
            Debug.Log("Raycast did not hit an object.");
        }
    }


    private void HandleBuildingInteraction(RaycastHit2D hit)
    {
        // Select building
        Building building = hit.collider.GetComponent<Building>();
        if (building != null)
        {
            SelectBuilding(building);
        }
        else
        {
            // Try to place building at hit position (might not be buildable terrain)
            Vector3 hitPosition = hit.point;
            BuildingManager buildingManager = FindObjectOfType<BuildingManager>();
            if (buildingManager != null)
            {
                Vector2Int gridPosition = buildingManager.GridPositionFromWorld(hitPosition);
                buildingManager.TryPlaceBuildingAt(gridPosition);
            }
        }
    }

    private void SelectBuilding(Building building)
    {
        Debug.Log("Selected building: " + building.name);
    }
}

