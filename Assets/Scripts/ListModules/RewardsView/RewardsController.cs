using System.Diagnostics;

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
        print(GameManager.Instance);
        print(GameManager.Instance.coins);
        Debug.Assert(GameManager.Instance != null, "GameManager is null");
        if (GameManager.Instance.coins >= reward.RewardCost)
        {
            CompleteListItem(reward);
        }
        else
        {
            // Prompt user to earn more coins
            /*
            uiController.ShowNotification("Insufficient coins", "You need more coins to unlock this reward.");
            */
        }
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


    public override void CompleteListItem(Reward reward)
    {
        // Deduct coins
        GameManager.Instance.coins -= reward.RewardCost;

        // Animate celebration for completing reward

        // Delete reward
        reward.TriggerOnDelete();
    }
}