using TMPro;
using UnityEngine;



public class RewardUIController : ListItemUIController<Reward, RewardData>
{
    [SerializeField] TMP_Text coinCost;
    public string CoinCost { get { return coinCost.text; } private set { coinCost.text = value;} }
    

    public override void UpdateUIValues(Reward reward)
    {
        TitleField = reward.ItemName;
        DescriptionField = reward.ItemDescription;
        TierField = (int)reward.RewardTier;
        CoinCost = reward.RewardCost.ToString();
    }


    public override void UpdateUIValues(RewardData reward)
    {
        TitleField = reward.itemName;
        DescriptionField = reward.itemDescription;
        TierField = (int)reward.tier;
        CoinCost = reward.coinCost.ToString();
    }
}
