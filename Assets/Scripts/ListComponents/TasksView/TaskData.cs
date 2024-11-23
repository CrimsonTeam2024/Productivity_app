using System;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class TaskData : ListItemData
{
    [SerializeField] private uint _timeCost = 10; // seconds
    [SerializeField] private TaskTier _taskTier = TaskTier.Medium;


    public uint TimeCost 
    { 
        get { return _timeCost; } 
        set { _timeCost = value; } 
    } 


    public TaskTier TaskTier 
    { 
        get { return _taskTier; } 
        set { _taskTier = value; } 
    } 


    public TaskData(Dictionary<string, string> keyValuePairs) : base() // TODO: come up with a better name for the input
    {
        foreach (var pair in keyValuePairs)
        {
            string key = pair.Key;

            if (key.Contains("Name"))
            {
                ItemName = pair.Value;
                continue;
            }

            if (key.Contains("Description"))
            {
                ItemDescription = pair.Value;
                continue;
            }

            if (key.Contains("Cost"))
            {
                if (uint.TryParse(pair.Value, out uint result))
                {
                    TimeCost = result;
                }
                continue;
            }

            if (key.Contains("Tier"))
            {
                if (Enum.TryParse(pair.Value, out TaskTier tier))
                {
                    TaskTier = tier;
                }
                continue;
            }
        }   
    }
}