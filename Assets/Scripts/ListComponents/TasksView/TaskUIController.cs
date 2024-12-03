using TMPro;
using UnityEngine;



public class TaskUIController : ListItemUIController<Task, TaskData>
{
    [SerializeField] TMP_Text timeCost;
    public string TimeCost { get { return timeCost.text; } private set { timeCost.text = value;} }
    

    public override void UpdateUIValues(Task task)
    {
        TitleField = task.ItemName;
        DescriptionField = task.ItemDescription;
        TierField = (int)task.TaskTier;
        TimeCost = task.TimeCost.ToString();
    }
}
