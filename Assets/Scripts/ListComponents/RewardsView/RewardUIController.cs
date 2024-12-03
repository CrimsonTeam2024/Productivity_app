using TMPro;
using UnityEngine;



public class RewardUIController : ListItemUIController<Reward, RewardData>
{
    [SerializeField] TMP_Text timeCost;
    public string TimeCost { get { return timeCost.text; } private set { timeCost.text = value;} }
    

    public override void UpdateUIValues(Reward reward)
    {
        TitleField = reward.ItemName;
        DescriptionField = reward.ItemDescription;
        TierField = (int)reward.RewardTier;
        TimeCost = reward.RewardCost.ToString();
    }
}
