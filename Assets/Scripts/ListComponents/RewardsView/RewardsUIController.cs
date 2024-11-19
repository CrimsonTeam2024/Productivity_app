public class RewardsUIController : ListUIController<Reward>
{
    // This is a work around for not being able to do new T() for generics.
    protected override Reward ConvertInputFieldsToListItem(Dictionary<string, string> dict)
    {
        return new Reward(dict);
    }
    public override void FillDetails(BaseListItem reward)
    {
        reward = (Reward)reward;

        // TODO: Make a reward details fill function 
    }
}