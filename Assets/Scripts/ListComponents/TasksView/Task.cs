public class Task : BaseListItem
{
    uint _timeCost; // seconds
    public uint TimeCost { get { return _timeCost; } set { _timeCost = value; } } 
    TaskTier _taskTier;
    public TaskTier TaskTier { get { return _taskTier; } set { _taskTier = value; } } 
}