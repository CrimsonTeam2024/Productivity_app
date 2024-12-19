using System;
using UnityEngine;

public class RewardsController : ListController<Reward, RewardData>
{
    RewardsUIController rewardsUIController;
    public PopUpBanner popUpBanner; 

    protected override void Start()
    {
        base.Start();
        newListItemUIController = uiController.newListItemPanel.GetComponent<NewRewardUIController>();
        editController = uiController.editListItemPanel.GetComponent<EditRewardUIController>();
        uiController.newListItemUIController = newListItemUIController;
        uiController.editController = editController;
    }

    public override void ActivateListItem(Reward reward)
    {
        Debug.Log(GameManager.Instance);
        print(GameManager.Instance.coins);

        if (GameManager.Instance.coins >= reward.RewardCost)
        {
            CompleteListItem(reward);
            popUpBanner.ShowBanner();
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