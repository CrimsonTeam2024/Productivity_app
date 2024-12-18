using TMPro;
using UnityEngine;



public class TaskUIController : ListItemUIController<Task, TaskData>
{
    [SerializeField] TMP_Text timeCost;
    public string TimeCost { get { return timeCost.text; } private set { timeCost.text = value; } }
    

    public override void UpdateUIValues(Task task)
    {
        TitleField = task.ItemName;
        DescriptionField = task.ItemDescription;
        TierField = (int)task.TaskTier;
        TimeCost = task.TimeCost.ToString();
    }
    

    public override void UpdateUIValues(TaskData taskData)
    {
        TitleField = taskData.itemName;
        DescriptionField = taskData.itemDescription;
        TierField = (int)taskData.tier;
        TimeCost = taskData.timeCost.ToString();
    }
}
