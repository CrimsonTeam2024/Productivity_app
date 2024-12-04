


public class EditRewardUIController : EditListItemPanelUIController<RewardData>
{
    // public Slider timeCostSlider;
    // SliderController sliderController;


    void Awake()
    {
        // sliderController = timeCostSlider.GetComponent<SliderController>();
    }


    public override RewardData GetListItemFromUI()
    {
        RewardData rewardData = new RewardData {
            itemName = itemTitle.text,
            itemDescription = itemDescription.text,
            tier = (RewardTier)itemTier.value,
            // coinCost = sliderController.Value
        };
        
        return rewardData;
    }


    public override void UpdateUIValues(RewardData rewardData)
    {
        itemTitle.text = rewardData.itemName;
        itemDescription.text = rewardData.itemDescription;
        itemTier.value = (int)rewardData.tier;
        // sliderController.Value = (int)rewardData.tier;
    }

    // Expose methods that validate whether user input in input fields is valid

    // Create methods for doing stuff with UI in the case of valid or invalid input
}