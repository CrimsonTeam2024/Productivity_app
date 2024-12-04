public class RewardsController : ListController<Reward, RewardData>
{
    RewardsUIController rewardsUIController;

    void Awake()
    {
        // Reward.OnDeleteReward += HandleDeleteItemFromList;
        // Reward.OnActivateReward += ActivateListItem;
        // Reward.OnEditReward += EditListItem;
        // Reward.OnInitEditReward += ShowEditListItemPanel;
    }
    

    public override void ActivateListItem(Reward reward)
    {
        // Check if user has enough coins to unlock reward

        // Prompt user to make sure they want to spend coins

        // If yes then call CompleteListItem()
    }

    public override void AddListItem(Reward reward, uint index)
    {
        base.AddListItem(reward, index);

        // Subscribe to instance-level events
        reward.OnActivateReward += ActivateListItem;
        reward.OnEditReward += EditListItem;
        reward.OnInitEditReward += ShowEditListItemPanel;
        reward.OnDeleteReward += HandleDeleteItemFromList;
    }


    public override void CompleteListItem()
    {
        // Deduct coins

        // Animate celebration for completing reward

        // Delete reward
    }
    

    public override void SelectListItem()
    {
        rewardsUIController.ShowDetailsPanel();
    }
}