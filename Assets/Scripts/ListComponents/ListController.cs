using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;

// This class
public abstract class ListController<T> : MonoBehaviour where T : ListItemData
{
    [SerializeField] List<T> list;
    public T selectedListItem; // The currently selected list item
    public bool toggleDetail = false;
    ListUIController<T> uiController;
    public GameManager gameManager;

    void Start()
    {
        uiController = GetComponent<ListUIController<T>>();
    }

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

    public void ShowNewListItem()
    {
        uiController.ShowCreateListItemPanel();
    }

    public void CreateNewListItem()
    {
        T listItem = uiController.CreateNewListItem();
        uiController.InstantiateNewListItem(listItem);
        AddListItem(listItem);
        uiController.HideCreateListItemPanel();
    }

    public void CancelCreateNewListItem()
    {
        uiController.HideCreateListItemPanel();
    }

    public void AddListItem(T listItem)
    {
        // TODO: Add list item
        list.Add(listItem);
    }


    public void DeleteListItem()
    {
        // TODO: Delete list item
    }


    public void EditListItem(T newItemData)
    {
        // TODO: Edit list item
    }


    public void DuplicateListItem()
    {
        // TODO: Duplicate list item
    }


    public void SelectListItem(BaseEventData data) // TODO: fix
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
                Debug.Log("Selected item set to: " + selectedListItem.ItemName);
            }
            else
            {
                Debug.LogWarning("The selected GameObject does not contain the expected list item component.");
            }
        }
    }

}