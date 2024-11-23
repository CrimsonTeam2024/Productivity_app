using UnityEngine;



public class Reward : MonoBehaviour
{
    public RewardData rewardData;

    public void Delete()
    {
        Destroy(gameObject);
    }
}