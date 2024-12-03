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
            // Otherwise assume drag, let the ScrollRect handle it
        }
    }

    private void HandleClick(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Convert screen pos to local RectTransform pos
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint);

        // Normalize local coords to [0,1]
        Vector2 normalizedPoint = new Vector2(
            (localPoint.x - rectTransform.rect.x) / rectTransform.rect.width,
            (localPoint.y - rectTransform.rect.y) / rectTransform.rect.height);

        // Can adjust for pivot
        normalizedPoint -= rectTransform.pivot;

        // Ray from villageCamera
        Ray ray = villageCamera.ViewportPointToRay(normalizedPoint + new Vector2(0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Interact with hit object
            Debug.Log("Hit object: " + hit.collider.gameObject.name);

            // Select building
            Building building = hit.collider.GetComponent<Building>();
            if (building != null)
            {
                // Selection etc
                SelectBuilding(building);
            }
            else
            {
                // Try to place building at hit position
                // (may not be buildable area)
                Vector3 hitPosition = hit.point;
                BuildingManager buildingManager = FindObjectOfType<BuildingManager>();
                if (buildingManager != null)
                {
                    Vector2Int gridPosition = buildingManager.GridPositionFromWorld(hitPosition);
                    buildingManager.TryPlaceBuildingAt(gridPosition);
                }
            }
        }
    }

    private void SelectBuilding(Building building)
    {
        // Implement your building selection logic here
        Debug.Log("Selected building: " + building.name);
    }
}
