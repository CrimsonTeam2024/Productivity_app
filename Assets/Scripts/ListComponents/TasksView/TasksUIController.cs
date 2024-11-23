using System.Collections.Generic;



public class TasksUIController : ListUIController<TaskData>
{
    // This is a work around for not being able to do new T() for generics.
    protected override TaskData ConvertInputFieldsToListItem(Dictionary<string, string> dict)
    {
        return new TaskData(dict);
    }

    public override void FillDetails(ListItemData task)
    {
        task = (TaskData)task;

        // TODO: Make a task details fill function 
    }
}