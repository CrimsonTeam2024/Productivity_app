public class RewardsUIController : ListUIController<Reward, RewardData>
{   

    void Awake()
    {
        newListItemUIController = newListItemPanel.GetComponent<NewRewardUIController>();
        editController = editListItemPanel.GetComponent<EditRewardUIController>();     
    }

    public override RewardData GetNewListItemDataFromUI()
    {
        return newListItemUIController.GetListItemFromUI();        
    }


    public override void ShowEditListItemPanel(Reward reward)
    {
        editListItemPanel.SetActive(true);
        editController.UpdateUIValues(reward.GetData());
        // TODO: After instantiation, animate the list item details screen pop up
        //       so that it looks good. This is strongly tied with design.
    }
}