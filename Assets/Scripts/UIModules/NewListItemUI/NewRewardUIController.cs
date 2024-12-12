using TMPro;



public class NewRewardUIController : NewListItemPanelUIController<RewardData>
{
    public TMP_Text displayCoinCost;


    void Awake()
    {
        itemTier.onValueChanged.AddListener(UpdateCoinCostDisplay);
    }


    void UpdateCoinCostDisplay(int selectedIndex) {
        int coins = (itemTier.value + 1) * 10;
        displayCoinCost.text = coins.ToString();
    }


    public override RewardData GetListItemFromUI()
    {
        RewardData rewardData = new RewardData {
            itemName = itemTitle.text,
            itemDescription = itemDescription.text,
            tier = (RewardTier)itemTier.value,
            coinCost = (itemTier.value + 1) * 10
        };

        return rewardData;
    }

}