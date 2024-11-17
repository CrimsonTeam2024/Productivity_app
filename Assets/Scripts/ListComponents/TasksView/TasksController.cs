using UnityEngine;

// This class
public class TasksController : ListController<Task>
{
    TasksUIController TasksUIController;

    void Awake()
    {
        TasksUIController = GetComponent<TasksUIController>();        
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
        TasksUIController.ShowDetailsPanel(selectedListItem);
    }
}