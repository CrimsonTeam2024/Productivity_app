public class RewardsController : ListController<Reward>
{
    RewardsUIController rewardsUIController;

    void Awake()
    {
        Reward.OnDeleteReward += HandleDeleteItemFromList;
    }
    

    public override void ActivateListItem(Reward reward)
    {
        // Check if user has enough coins to unlock reward

        // Prompt user to make sure they want to spend coins

        // If yes then call CompleteListItem()
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