using UnityEngine;
using UnityEngine.EventSystems;

// This class
public abstract class ListItemController<T> : MonoBehaviour where T : BaseListItem
{
    public T selectedListItem; // The currently selected list item
    public UIManager uiManager;
    public bool toggleDetail = false;
    public GameObject detailObject;


    // What the item does when "started"
    public abstract void Init();

    // What happens when user completes list item
    public abstract void Complete();

    // Reveal list item details view
    public void RevealDetails()
    {
        if (!toggleDetail)
            detailObject = uiManager.ShowDetails<T>(selectedListItem);
        else
            uiManager.HideDetails(detailObject);
        
        toggleDetail = !toggleDetail;
        Canvas.ForceUpdateCanvases();
    }
    
    // Hide list item details view
    public void HideDetails()
    {
        uiManager.HideDetails(detailObject);
    }


    public void Add(T itemData)
    {
        // Add item to list
    }


    public void Delete()
    {
        // Delete task object
    }


    public void Edit(T newItemData)
    {
        // Change item data
    }


    public void Duplicate()
    {
        // Duplicate list item - just user-friendliness
    }


    public void SelectListItem(BaseEventData data)
    {
        
        // Cast the BaseEventData to PointerEventData to get access to pointer details
        PointerEventData pointerData = data as PointerEventData;
        
        if (pointerData != null)
        {
            // Get the GameObject that was clicked on or hovered over
            GameObject selectedObject = pointerData.pointerEnter;

            // Try to get the T component (list item) from the selected GameObject
            T listItem = selectedObject.GetComponent<T>();
            
            // If the list item exists, set it as the selected list item
            if (listItem != null)
            {
                selectedListItem = listItem;
                Debug.Log("Selected item set to: " + selectedListItem.name);
            }
            else
            {
                Debug.LogWarning("The selected GameObject does not contain the expected list item component.");
            }
        }
    }

}