using UnityEngine;

// This class
public class RewardController : ListItemController<Reward>
{
    public override void Init()
    {
        // Check if user has enough coins to unlock reward

        // Prompt user to make sure they want to spend coins

        // If yes then call Complete()
    }

    public override void Complete()
    {
        // Deduct coins

        // Animate celebration for completing reward
    }
}