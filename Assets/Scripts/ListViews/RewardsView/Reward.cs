public class Reward : BaseListItem
{
    uint RewardCost { get; set; } // Coin cost

    public RewardTier Tier { get; set; }

    public Reward(string name, string description, uint cost)
    {
        itemName = name;
        itemDescription = description;
        RewardCost = cost;
    }
}