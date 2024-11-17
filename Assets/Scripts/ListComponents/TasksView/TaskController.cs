using UnityEngine;

// This class
public class TaskController : ListItemController<Task>
{
    TaskUIController taskUIController;

    void Awake()
    {
        taskUIController = GetComponent<TaskUIController>();        
    }

    public override void ActivateListItem()
    {
        // Opens village manager view and prompts to select build tasks

        // After selected, prompt to activate task

        // If activate chosen, create focus session based on task data
    }

    public override void CompleteListItem()
    {
        // Reward with coins and xp
    }

    public override void SelectListItem()
    {
        taskUIController.ShowDetailsPanel(selectedListItem);
    }
}