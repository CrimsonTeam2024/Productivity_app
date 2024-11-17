using UnityEngine;

// This class
public class RewardController : ListItemController<Reward>
{
    RewardUIController rewardUIController;

    void Awake()
    {
        rewardUIController = GetComponent<RewardUIController>();        
    }
    public override void ActivateListItem()
    {
        // Check if user has enough coins to unlock reward

        // Prompt user to make sure they want to spend coins

        // If yes then call CompleteListItem()
    }

    public override void CompleteListItem()
    {
        // Deduct coins

        // Animate celebration for completing reward
    }

    public override void SelectListItem()
    {
        rewardUIController.ShowDetailsPanel(selectedListItem);
    }
}