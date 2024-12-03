public class TasksUIController : ListUIController<Task, TaskData>
{
    NewListItemPanelUIController<TaskData> newTaskUIController;
    
    public override TaskData GetNewListItemDataFromUI()
    {
        newTaskUIController = newListItemPanel.GetComponent<NewTaskUIController>();
        return newTaskUIController.GetListItemFromUI();
    }
}