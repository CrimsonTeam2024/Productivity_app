using System.Collections.Generic;

public class RewardsUIController : ListUIController<RewardData>
{
    // This is a work around for not being able to do new T() for generics.
    protected override RewardData ConvertInputFieldsToListItem(Dictionary<string, string> dict)
    {
        return new RewardData(dict);
    }

    public override void FillDetails(ListItemData reward)
    {
        reward = (RewardData)reward;

        // TODO: Make a reward details fill function 
    }
}