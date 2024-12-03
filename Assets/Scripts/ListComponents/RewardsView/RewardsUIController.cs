public class RewardsUIController : ListUIController<Reward, RewardData>
{
    NewListItemPanelUIController<RewardData> newRewardUIController;
    public override RewardData GetNewListItemDataFromUI()
    {
        newRewardUIController = newListItemPanel.GetComponent<NewListItemPanelUIController<RewardData>>();
        return newRewardUIController.GetListItemFromUI();        
    }
}