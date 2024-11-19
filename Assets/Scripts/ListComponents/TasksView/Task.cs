using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEngine;

public class Task : BaseListItem
{
    [SerializeField] uint _timeCost = 10; // seconds
    public uint TimeCost { get { return _timeCost; } set { _timeCost = value; } } 
    TaskTier _taskTier = TaskTier.Medium;
    public TaskTier TaskTier { get { return _taskTier; } set { _taskTier = value; } } 

    public Task(Dictionary<string, string> keyValuePairs) // TODO: come up with a better name for the input
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