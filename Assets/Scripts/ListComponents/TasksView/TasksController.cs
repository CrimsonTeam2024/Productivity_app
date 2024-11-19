using System;
using System.Xml.XPath;
using UnityEngine;

// This class
public class TasksController : ListController<TaskData>
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
        // Provisional solution.
        // TODO: Come up with a better way of converting completed tasks to coins and xp
        // Reward with coins and xp
        switch (selectedListItem.TaskTier)
        {
            case TaskTier.Easy:
                // Logic for easy tasks
                gameManager.coins += 2;
                gameManager.xp += 100; 
                break;

            case TaskTier.Medium:
                // Logic for medium tasks
                gameManager.coins += 5;
                gameManager.xp += 200;
                break;

            case TaskTier.Hard:
                // Logic for hard tasks
                gameManager.coins += 10;
                gameManager.xp += 500;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(selectedListItem.TaskTier), "Unexpected TaskTier value.");
        }
    }


    public override void SelectListItem()
    {
        TasksUIController.ShowDetailsPanel(selectedListItem);
    }
}