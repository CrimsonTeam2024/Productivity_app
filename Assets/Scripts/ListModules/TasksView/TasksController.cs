using System;



// This class
public class TasksController : ListController<Task, TaskData>
{
    public FocusController focusController;


    protected override void Start()
    {
        base.Start();
        newListItemUIController = uiController.newListItemPanel.GetComponent<NewTaskUIController>();
        editController = uiController.editListItemPanel.GetComponent<EditTaskUIController>();
        uiController.newListItemUIController = newListItemUIController;
        uiController.editController = editController;
    }


    // TODO: Implement
    public override void ActivateListItem(Task activatedTask)
    {
        focusController.gameObject.SetActive(true);
        focusController.StartFocusTimer(activatedTask);
        // Opens village manager view and prompts to select build tasks

        // After selected, prompt to activate task

        // If activate chosen, create focus session based on task data
    }


    // TODO: List items should do something when completed
    public override void CompleteListItem()
    {
        // Provisional solution.
        // TODO: Come up with a better way of converting completed tasks to coins and xp
        // Reward with coins and xp
        switch (selectedListItem.TaskTier)
        {
            case TaskTier.Easy:
                // Logic for easy tasks
                GameManager.Instance.coins += 2;
                GameManager.Instance.xp += 100; 
                break;

            case TaskTier.Medium:
                // Logic for medium tasks
                GameManager.Instance.coins += 5;
                GameManager.Instance.xp += 200;
                break;

            case TaskTier.Hard:
                // Logic for hard tasks
                GameManager.Instance.coins += 10;
                GameManager.Instance.xp += 500;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(selectedListItem.TaskTier), "Unexpected TaskTier value.");
        }
    }

    public override void AddListItem(Task task, uint index)
    {
        base.AddListItem(task, index);

        // Subscribe to instance-level events
        task.OnActivateTask += ActivateListItem;
        task.OnEditTask += EditListItem;
        task.OnInitEditTask += ShowEditListItemPanel;
        task.OnDeleteTask += HandleDeleteItemFromList;
    }

    public override void DeleteListItem(Task task)
    {
        base.DeleteListItem(task);

        // Subscribe to instance-level events
        task.OnActivateTask -= ActivateListItem;
        task.OnEditTask -= EditListItem;
        task.OnInitEditTask -= ShowEditListItemPanel;
        task.OnDeleteTask -= HandleDeleteItemFromList;
    }
}