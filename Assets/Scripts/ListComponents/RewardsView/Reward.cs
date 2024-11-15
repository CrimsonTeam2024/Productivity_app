public class Reward : BaseListItem
{
    uint _rewardCost;
    uint RewardCost { get { return _rewardCost; } set { _rewardCost = value; } } // Coin cost

    RewardTier _tier;
    public RewardTier Tier { get { return _tier; } set { _tier = value; } }

    public Reward(string name, string description, uint cost)
    {
        ItemName = name;
        ItemDescription = description;
        RewardCost = cost;
    }
}