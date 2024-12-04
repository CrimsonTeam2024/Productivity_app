using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public abstract class ListController<T, U> : MonoBehaviour where T : ListItem<U> where U : ListItemData
{
    public List<T> list;
    public T selectedListItem; // The currently selected list item
    public bool toggleDetail = false;
    protected ListUIController<T, U> uiController;
    public GameManager gameManager;
    public uint atIndex = 0;


    void Start()
    {
        uiController = GetComponent<ListUIController<T, U>>();
        uiController.list = list;
    }


    // What the item does when "started"
    public abstract void ActivateListItem(T ListItem);


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


    public void ShowNewListItemPanel()
    {
        uiController.ShowNewListItemPanel();
    }


    public void ShowEditListItemPanel(T selectedListItem)
    {
        uiController.ShowEditListItemPanel(selectedListItem);
        uiController.BindDeleteButton(selectedListItem);
        uiController.BindSaveButton(selectedListItem);
    }


    public void CreateNewListItem()
    {    
        T listItem = uiController.InstantiateNewListItem(atIndex);

        // TODO: Populate listItem with data from the NewListItemPanel input fields
        // Dictionary<string, string> temp = uiController.CreateNewListItem();
        U data = uiController.GetNewListItemDataFromUI();
        listItem.SetData(data);

        AddListItem(listItem, atIndex);

        uiController.UpdateListItemTargetPositions(list);

        uiController.HideCreateListItemPanel();
    }


    public void CancelCreateNewListItem()
    {
        uiController.HideCreateListItemPanel();
    }


    public void CancelEditListItem()
    {
        uiController.HideEditListItemPanel();
        uiController.ResetEditListItemPanel();
    }


    public virtual void AddListItem(T listItem, uint index)
    {
        if (index > list.Count)
        {
            Debug.LogError("List item index out of range.");
            return;
        }

        listItem.Index = index;

        list.Insert((int)index, listItem);
        UpdateListIndices();
    }


    protected void UpdateListIndices()
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].Index = (uint)i;
        }
    }


    // This method is triggered when the OnDeleteFromList global event fires
    public void HandleDeleteItemFromList(T listItem, GameObject listItemGameObject)
    {
        int id = (int)listItem.Index;
        list.RemoveAt(id);
        UpdateListIndices();
        Destroy(listItemGameObject);
        if (list.Count > 0)
        {
            uiController.UpdateListItemTargetPositions(list);
        }

        DeleteListItem(listItem);
    }


    public virtual void DeleteListItem(T listItem)
    {
        // TODO: Delete list item
    }


    public void EditListItem(T selectedListItem)
    {
        U newListItemData = uiController.editController.GetListItemFromUI();
        selectedListItem.SetData(newListItemData);
        
        ListItemUIController<T, U> itemUIController = selectedListItem.GetComponent<ListItemUIController<T, U>>();
        itemUIController.UpdateUIValues(newListItemData);
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