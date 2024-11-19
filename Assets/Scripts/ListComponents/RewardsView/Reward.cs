using System;
using System.Collections.Generic;

public class Reward : BaseListItem
{
    uint _rewardCost;
    uint RewardCost { get { return _rewardCost; } set { _rewardCost = value; } } // Coin cost

    RewardTier _tier;
    public RewardTier RewardTier { get { return _tier; } set { _tier = value; } }

    public Reward(string name, string description, uint cost)
    {
        ItemName = name;
        ItemDescription = description;
        RewardCost = cost;
    }

    public Reward(Dictionary<string, string> keyValuePairs) // TODO: come up with a better name for the input
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