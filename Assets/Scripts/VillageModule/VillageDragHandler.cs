using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class VillageDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Drag Settings")]
    public float dragThreshold = 10f; // Minimum 'mouse-down' distance before considered an "actaul" drag

    private RectTransform rectTransform;
    private Vector2 startDragPosition; 
    private Vector2 lastDragPosition;
    Vector2 delta;
    Coroutine inertiaCoroutine;
    public float inertiaDuration = 3f;
    [Range (0, 1)] 
    public float inertiaDampingFactor = 0.5f;
    private bool isDragging;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag at " + eventData.position);
        
        isDragging = false;
        startDragPosition = eventData.position;
        lastDragPosition = eventData.position;
        if (inertiaCoroutine != null)
            StopCoroutine(inertiaCoroutine);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag at " + eventData.position);
        
        float distance = Vector2.Distance(eventData.position, startDragPosition);

        // If surpasses distance threshold -> registered as 'actual' drag
        if (!isDragging && distance > dragThreshold)
        {
            Debug.Log("Now dragging the village!");
            isDragging = true;
        }

        if (isDragging)
        {
            // Move "Village" obj (per its rectTransform or whatever) by the pointer position difference
            delta = eventData.position - lastDragPosition;
            lastDragPosition = eventData.position;

            // Shifts anchor position of that transform
            rectTransform.anchoredPosition += delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag at " + eventData.position);
        if (isDragging)
        {
            inertiaCoroutine = StartCoroutine(ApplyInertia());
        }
    }

    private IEnumerator ApplyInertia()
    {
        float elapsedTime = 0f;
        Vector2 currentDelta = delta;

        while (elapsedTime < inertiaDuration)
        {
            rectTransform.anchoredPosition += currentDelta;
            currentDelta = Vector2.Lerp(currentDelta, Vector2.zero, inertiaDampingFactor);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
