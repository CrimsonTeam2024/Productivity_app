using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Manager class attached to some manager GameObject that manages 
public abstract class ListUIController<T, U> : MonoBehaviour where T : ListItem<U> where U : ListItemData
{
    public List<T> list;
    public GameObject newListItemPanel;
    public GameObject editListItemPanel;
    public GameObject listItemPrefab; // The actual list item prefab
    public GameObject listContainer;
    public float listPadding; // List spacing

    RectTransform listContainerRect;
    public float listTopPadding;
    public float listBottomPadding;
    float listItemHeight;
    
    [HideInInspector] public NewListItemPanelUIController<U> newListItemUIController;
    [HideInInspector] public EditListItemPanelUIController<U> editController;
    
    
    void Start()
    {
        newListItemPanel.SetActive(false);
        editListItemPanel.SetActive(false);

        listContainerRect = listContainer.GetComponent<RectTransform>();    
        RectTransform rect = listItemPrefab.transform as RectTransform;
        listItemHeight = rect.sizeDelta.y;
    }


    void Update()
    {
        // UpdateListItemPositions(list);
    }


    public void ShowNewListItemPanel()
    {
        newListItemPanel.SetActive(true);
        // TODO: After instantiation, animate the list item details screen pop up
        //       so that it looks good. This is strongly tied with design.
    }


    public abstract void ShowEditListItemPanel(T listItem);
    
    
    public void BindDeleteButton(T listItem)
    {
        editController.deleteButton.onClick.AddListener(HideEditListItemPanel);
        editController.deleteButton.onClick.AddListener(listItem.TriggerOnDelete);
    }


    public void BindSaveButton(T listItem)
    {
        editController.saveButton.onClick.AddListener(HideEditListItemPanel);
        editController.saveButton.onClick.AddListener(listItem.TriggerOnEdit);
    }


    public void HideCreateListItemPanel()
    {
        newListItemPanel.SetActive(false);
        // TODO: Should also reset the newListItemPanel input text
        // TODO: After instantiation, animate the reward details screen pop up
    }


    public void HideEditListItemPanel()
    {
        editListItemPanel.SetActive(false);
        // TODO: Should also reset the newListItemPanel input text
        // TODO: After instantiation, animate the reward details screen pop up
    }


    public void ResetEditListItemPanel()
    {
        editController.saveButton.onClick.RemoveAllListeners();
        editController.deleteButton.onClick.RemoveAllListeners();
    }


    public abstract U GetNewListItemDataFromUI();


    public T InstantiateNewListItem(uint index)
    {
        GameObject newListItem = Instantiate(listItemPrefab, listContainer.transform);
        T listItem = newListItem.GetComponent<T>();
        listItem.Index = index;
        listItem.UpdateTargetPosition(listTopPadding, listPadding);
        listItem.SnapToTargetPosition();
        return listItem;
    }

    
    public void UpdateListItemTargetPositions(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogError("List cannot be null or empty when updating item positions.");
            return;
        }

        foreach (T item in list)
        {
            if (item == null)
            {
                Debug.LogWarning("Null item found in the list; skipping.");
                continue;
            }

            // Calculate new position in 2D space
            item.UpdateTargetPosition(listTopPadding, listPadding);
        }
        
        UpdateListContainer();
    }


    private void UpdateListContainer()
    {
        Vector2 sizeDelta = listContainerRect.sizeDelta;
        sizeDelta.y = listTopPadding + list.Count * (listItemHeight + listPadding) + listBottomPadding;
        listContainerRect.sizeDelta = sizeDelta;
    }
}