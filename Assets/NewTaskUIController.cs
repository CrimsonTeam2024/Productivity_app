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

    // Expose methods that validate whether user input in input fields is valid

    // Create methods for doing stuff with UI in the case of valid or invalid input
}
