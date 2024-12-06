using TMPro;
using UnityEngine;



public class NewRewardUIController : NewListItemPanelUIController<RewardData>
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

}