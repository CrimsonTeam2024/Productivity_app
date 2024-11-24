using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemListManager : MonoBehaviour
{
    public GameObject itemDialogBox;
    public Button addItemButton;
    public Button cancelButton;
    public Button saveButton;
    public TMP_InputField titleInputField;
    public TMP_InputField descriptionInputField;
    public Transform listContent;
    public GameObject listItemPrefab;

    private void Start()
    {
        // Assign button click listeners
        addItemButton.onClick.AddListener(OpenDialogBox);
        cancelButton.onClick.AddListener(CloseDialogBox);
        saveButton.onClick.AddListener(SaveItem);
    }

    private void OpenDialogBox()
    {
        itemDialogBox.SetActive(true);
    }

    private void CloseDialogBox()
    {
        Debug.Log("CoseDialogueBox()");
        itemDialogBox.SetActive(false);
        ClearDialogBoxFields();
    }

    private void SaveItem()
    {
        string title = titleInputField.text;
        string description = descriptionInputField.text;

        if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description))
        {
            // Instantiate a new list item
            GameObject newItem = Instantiate(listItemPrefab, listContent);

            // ...set its text fields
            TMP_Text titleText = newItem.transform.Find("ItemTitleText").GetComponent<TMP_Text>();
            TMP_Text descriptionText = newItem.transform.Find("ItemDescriptionText").GetComponent<TMP_Text>();
            titleText.text = title;
            descriptionText.text = description;
            newItem.SetActive(true);

            // ...set its position one above AddItemListItem
            int addButtonIndex = listContent.Find("AddItemListItem").GetSiblingIndex();
            newItem.transform.SetSiblingIndex(addButtonIndex);
        }
        else
        {
            Debug.LogWarning("Title or Description empty!");
        }

        // Close the dialog box
        CloseDialogBox();
    }


    private void ClearDialogBoxFields()
    {
        titleInputField.text = string.Empty;
        descriptionInputField.text = string.Empty;
    }
}
