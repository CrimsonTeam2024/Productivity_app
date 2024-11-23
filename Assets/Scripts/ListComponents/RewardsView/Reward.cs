using System;
using System.Collections.Generic;
using UnityEngine;



public class Reward : ListItem
{
    [SerializeField] private uint _rewardCost; // Coin cost
    [SerializeField] private RewardTier _tier;

    public uint RewardCost
    {
        get { return _rewardCost; }
        set { _rewardCost = value; }
    }

    public RewardTier RewardTier
    {
        get { return _tier; }
        set { _tier = value; }
    }


    // Initialization method for dynamic setup
    public void Initialize(Dictionary<string, string> keyValuePairs)
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
                    RewardCost = result;
                }
                continue;
            }

            if (key.Contains("Tier"))
            {
                if (Enum.TryParse(pair.Value, out RewardTier tier))
                {
                    RewardTier = tier;
                }
                continue;
            }
        }
    }
}