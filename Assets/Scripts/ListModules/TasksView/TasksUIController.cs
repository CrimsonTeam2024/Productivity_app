public class TasksUIController : ListUIController<Task, TaskData>
{
    void Awake()
    {
    }


    public override TaskData GetNewListItemDataFromUI()
    {
        return newListItemUIController.GetListItemFromUI();
    }


    public override void ShowEditListItemPanel(Task task)
    {
        editListItemPanel.SetActive(true);
        editController.UpdateUIValues(task.GetData());
        // TODO: After instantiation, animate the list item details screen pop up
        //       so that it looks good. This is strongly tied with design.
    }
}