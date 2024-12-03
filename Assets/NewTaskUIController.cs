using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class NewTaskUIController : NewListItemPanelUIController<TaskData>
{
    public Slider timeCostSlider;
    SliderController sliderController;

    void Awake()
    {
        sliderController = timeCostSlider.GetComponent<SliderController>();
    }
    public override TaskData GetListItemFromUI()
    {
        TaskData taskData = new TaskData {
            itemName = itemTitle.text,
            itemDescription = itemDescription.text,
            tier = (TaskTier)itemTier.value,
            timeCost = sliderController.Value
        };
        
        return taskData;
    }


    // public override void SaveButtonOnClick()
    // {
    //     TaskItemData taskData = new TaskItemData() {
    //         title = itemTitle.text,
    //         description = itemDescription.text,
    //         tier = (TaskTier)itemTier.value,
    //         // timeCost = timeCostSlider.value;
    //     };

    //     // Validate inputs (you can customize this part as needed)
    //     if (string.IsNullOrWhiteSpace(taskData.title))
    //     {
    //         Debug.LogWarning("Title cannot be empty.");
    //         return;
    //     }

    //     if (string.IsNullOrWhiteSpace(taskData.description))
    //     {
    //         Debug.LogWarning("Description cannot be empty.");
    //         return;
    //     }

    //     // Call a method to handle the data (to be implemented in the subclass)
    //     HandleSave();
    // }

    // protected void HandleSave()
    // {
    //     tasksController.CreateNewListItem(TaskItemData taskData);
    // }

    // Get all the input fields (text or otherwise) from the NewListItemPanel GameObject

    // Expose methods that validate whether user input in input fields is valid

    // Create methods for doing stuff with UI in the case of valid or invalid input
}
