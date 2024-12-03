using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomScrollRect : ScrollRect
// override ScrollRect component to prevent its interference
// with event handling related to its content objects
{
    public bool routeToParent = false;

    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (!routeToParent)
            base.OnInitializePotentialDrag(eventData);
        else
            PassEventToParent(eventData, ExecuteEvents.initializePotentialDrag);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!routeToParent)
            base.OnDrag(eventData);
        else
            PassEventToParent(eventData, ExecuteEvents.dragHandler);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!routeToParent)
            base.OnBeginDrag(eventData);
        else
            PassEventToParent(eventData, ExecuteEvents.beginDragHandler);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (!routeToParent)
            base.OnEndDrag(eventData);
        else
            PassEventToParent(eventData, ExecuteEvents.endDragHandler);
    }

    private void PassEventToParent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function)
        where T : IEventSystemHandler
    {
        var parent = transform.parent;
        while (parent != null)
        {
            if (ExecuteEvents.Execute(parent.gameObject, data, function))
                break;
            parent = parent.parent;
        }
    }
}
