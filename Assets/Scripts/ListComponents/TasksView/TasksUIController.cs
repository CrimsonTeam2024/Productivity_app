using System.Collections.Generic;

public class TasksUIController : ListUIController<Task>
{
    // This is a work around for not being able to do new T() for generics.
    protected override Task ConvertInputFieldsToListItem(Dictionary<string, string> dict)
    {
        return new Task(dict);
    }

    public override void FillDetails(BaseListItem task)
    {
        task = (Task)task;

        // TODO: Make a task details fill function 
    }
}