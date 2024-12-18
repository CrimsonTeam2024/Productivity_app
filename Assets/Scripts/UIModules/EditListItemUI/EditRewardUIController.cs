using TMPro;


public class EditRewardUIController : EditListItemPanelUIController<RewardData>
{
    public TMP_Dropdown tierDropdown;
    public TMP_Text displayCoinCost;


    void Awake()
    {
        tierDropdown.GetComponent<TMP_Dropdown>();
        tierDropdown.onValueChanged.AddListener(UpdateCoinCostDisplay);
    }

    void UpdateCoinCostDisplay(int selectedIndex) {
        int coins = (tierDropdown.value + 1) * 10;
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


    public override void UpdateUIValues(RewardData rewardData)
    {
        itemTitle.text = rewardData.itemName;
        itemDescription.text = rewardData.itemDescription;
        itemTier.value = (int)rewardData.tier;
        displayCoinCost.text = ((itemTier.value + 1) * 10).ToString();
    }

    // Expose methods that validate whether user input in input fields is valid

    // Create methods for doing stuff with UI in the case of valid or invalid input
}