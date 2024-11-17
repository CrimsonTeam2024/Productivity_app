using UnityEngine;
using UnityEngine.EventSystems;

// This class
public abstract class ListController<T> : MonoBehaviour where T : BaseListItem
{
    public T selectedListItem; // The currently selected list item
    public bool toggleDetail = false;
    ListUIController uiController;


    // What the item does when "started"
    public abstract void ActivateListItem();

    // What happens when user completes list item
    public abstract void CompleteListItem();

    // Reveal list item details view on list item select
    public abstract void SelectListItem();
    
    // Hide list item details view
    public void DeselectListItem()
    {
        uiController.HideDetails();
        selectedListItem = null;
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