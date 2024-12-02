using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Manager class attached to some manager GameObject that manages 
public abstract class ListUIController<T> : MonoBehaviour where T : ListItem
{
    public List<T> list;
    public GameObject listItemDetailsPanel;
    public GameObject newListItemPanel;
    public GameObject listItemPrefab; // The actual list item prefab
    public GameObject listContainer;
    public float listPadding; // List spacing

    RectTransform listContainerRect;
    public float listTopPadding;
    public float listBottomPadding;
    float listItemHeight;

    void Awake()
    {
        listItemDetailsPanel.SetActive(false);
        newListItemPanel.SetActive(false);
    }

    void Start()
    {
        listContainerRect = listContainer.GetComponent<RectTransform>();    
        RectTransform rect = listItemPrefab.transform as RectTransform;
        listItemHeight = rect.sizeDelta.y;
    }


    void Update()
    {
        // UpdateListItemPositions(list);
    }


    public void ShowDetailsPanel()
    {
        // TODO: Manipulate transform position such that details panel is well positioned
        
        listItemDetailsPanel.SetActive(true);
        // TODO: After instantiation, animate the reward details screen pop up
    }


    // Method to hide details
    public void HideDetails()
    {
        if (listItemDetailsPanel != null)
        {
            Destroy(listItemDetailsPanel);
        }
    }


    public void ShowNewListItemPanel()
    {
        newListItemPanel.SetActive(true);
        // TODO: After instantiation, animate the list item details screen pop up
        //       so that it looks good. This is strongly tied with design.
    }


    public void HideCreateListItemPanel()
    {
        newListItemPanel.SetActive(false);
        // TODO: Should also reset the newListItemPanel input text
        // TODO: After instantiation, animate the reward details screen pop up
    }


    public Dictionary<string, string> CreateNewListItem()
    {
        if (!newListItemPanel.activeInHierarchy)
        {
            Debug.LogError("Cannot create new list item because "
                            + "the UI view responsible for this is not active in the heirarchy.");
            return null;
        }  

        TMP_InputField[] inputFields = newListItemPanel.GetComponentsInChildren<TMP_InputField>();
        Dictionary<string, string> placeholderToTextMap = new Dictionary<string, string>();
        
        // Generate a dictionary of input field names and their values, 
        // and match them to Task or Reward properties
        foreach (TMP_InputField inputField in inputFields)
        {
            if (inputField.placeholder is TMP_Text placeholderText)
            {
                string placeholder = placeholderText.text;
                string inputText = string.IsNullOrEmpty(inputField.text) ? placeholder : inputField.text;

                if (!placeholderToTextMap.ContainsKey(placeholder))
                {
                    placeholderToTextMap[placeholder] = inputText;
                }
            }
            else
            {
                Debug.LogWarning("Placeholder is not a TMP_Text component.");
            }
        }

        return placeholderToTextMap;
    }


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