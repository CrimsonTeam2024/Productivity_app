using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

// Manager class attached to some manager GameObject that manages 
public abstract class ListUIController<T> : MonoBehaviour where T : BaseListItem
{
    public GameObject listItemDetailsPanel;
    public GameObject newListItemPanel;
    ListController<T> listController;

    void Awake() {
        // Hide the action specific ui panels
        listItemDetailsPanel.SetActive(false);
        newListItemPanel.SetActive(false);

        listController = GetComponent<ListController<T>>();
    }


    public void ShowDetailsPanel(BaseListItem listItem)
    {
        Transform listItemTransform = listItem.GetComponentInParent<Transform>();
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


    public void ShowCreateListItemPanel()
    {
        newListItemPanel.SetActive(true);
        // TODO: After instantiation, animate the reward details screen pop up
    }


    public void HideCreateListItemPanel()
    {
        newListItemPanel.SetActive(false);
        // TODO: Should also reset the newListItemPanel input text
        // TODO: After instantiation, animate the reward details screen pop up
    }


    public void CreateNewListItem()
    {
        if (!newListItemPanel.activeInHierarchy)
        {
            Debug.LogError("Cannot create new list item because "
                            + "the UI view responsible for this is not active in the heirarchy.");
            return;
        }

        string itemName = string.Empty;
        string itemDescription = string.Empty;        

        TMP_InputField[] inputFields = newListItemPanel.GetComponentsInChildren<TMP_InputField>();
        Dictionary<string, string> placeholderToTextMap = new Dictionary<string, string>();
        
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

        T listItem = ConvertInputFieldsToListItem(placeholderToTextMap);

        // Create new Task here
        listController.AddListItem(listItem);
    }


    protected abstract T ConvertInputFieldsToListItem(Dictionary<string, string> dict);
    

    public abstract void FillDetails(BaseListItem listItem);
}