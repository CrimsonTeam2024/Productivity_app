using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemListManager : MonoBehaviour
{
    public Button addItemButton;

    // Dialog Box (for task assignment)
    public GameObject itemDialogBox;
    public Button cancelButton;
    public Button saveButton;
    public TMP_InputField titleInputField;
    public TMP_InputField descriptionInputField;
    public TMP_Dropdown categoryDropdown; // for task difficulty delection
    public Slider timeSlider; // Select task duration
    public TMP_Text sliderValueText;

    public Transform listContent;
    public GameObject listItemPrefab;

    private Color col_1 = new Color(30, 240, 140);
    private Color col_2 = new Color(255, 180, 0);
    private Color col_3 = new Color(255, 40, 20);

    private void Start()
    {
        // Click listener for 'add' button
        addItemButton.onClick.AddListener(OpenDialogBox);
        // Listeners for dialog box (hidden by default)
        cancelButton.onClick.AddListener(CloseDialogBox);
        saveButton.onClick.AddListener(SaveItem);
        categoryDropdown.onValueChanged.AddListener(OnCategoryChanged);
        timeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnCategoryChanged(int index)
    {
        // Maximum value is set per category
        switch(index)
        {
            case 0: 
                timeSlider.maxValue = 10; 
                break;
            case 1:
                timeSlider.maxValue = 30;
                break;
            case 2:
                timeSlider.maxValue = 60;
                break;
        }
        // after change of limits, reset slider to 0 to prevent any weird stuff
        timeSlider.value = timeSlider.minValue = 0;
        UpdateSliderValueText();
    }

    private void OnSliderValueChanged(float value)
    {
        UpdateSliderValueText();
    }
    private void UpdateSliderValueText()
    {
        sliderValueText.text = $"{Mathf.RoundToInt(timeSlider.value)}m";
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
        int category = categoryDropdown.value + 1;
        float time = timeSlider.value;

        if (!string.IsNullOrEmpty(title))
        {
            // Instantiate a new list item
            GameObject newItem = Instantiate(listItemPrefab, listContent);

            // Set text fields
            TMP_Text titleText = newItem.transform.Find("ItemTitleText").GetComponent<TMP_Text>();
            TMP_Text descriptionText = newItem.transform.Find("ItemDescriptionText").GetComponent<TMP_Text>();
            titleText.text = title;
            descriptionText.text = description;

            // Set difficulty-category and time-duration fields
            Transform categoryIndicator = newItem.transform.Find("IndicatorWrapper/CategoryIndicator");
            TMP_Text categoryText = categoryIndicator.Find("CategoryText")?.GetComponent<TMP_Text>();
            Image categoryBackground = categoryIndicator.GetComponent<Image>();
            Transform timeIndicator = newItem.transform.Find("IndicatorWrapper/TimeIndicator");
            TMP_Text timeText = timeIndicator.Find("TimeText")?.GetComponent<TMP_Text>();

            // Set category text and associated background colour
            categoryText.text = category.ToString();
            switch (category)
            {
                case 1:
                    categoryBackground.color = col_1;
                    break;
                case 2:
                    categoryBackground.color = col_2;
                    break;
                case 3:
                    categoryBackground.color = col_3;
                    break;
            }
            timeText.text = $"{Mathf.RoundToInt(time)}m";

            // ...set this item's position one above the 'add' button in the list
            int addButtonIndex = listContent.Find("AddItemListItem").GetSiblingIndex();
            newItem.transform.SetSiblingIndex(addButtonIndex);

            // Item is cloned from 'inactive' prefab - must make clone active again
            newItem.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Task item must have a title");
        }

        // Close the dialog box
        CloseDialogBox();
    }


    private void ClearDialogBoxFields()
    {
        titleInputField.text = string.Empty;
        descriptionInputField.text = string.Empty;
        categoryDropdown.value = 0;
        timeSlider.value = timeSlider.minValue;
        UpdateSliderValueText();
    }
}
