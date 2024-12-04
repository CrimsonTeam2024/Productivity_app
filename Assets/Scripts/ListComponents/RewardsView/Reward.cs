using System;
using System.Collections.Generic;
using UnityEngine;



public class Reward : ListItem<RewardData>
{
    public event Action<Reward> OnActivateReward;
    public event Action<Reward> OnEditReward;
    public event Action<Reward> OnInitEditReward;
    public event Action<Reward, GameObject> OnDeleteReward;
    RewardUIController uiController;
    
    [SerializeField] private uint _rewardCost; // Coin cost
    [SerializeField] private RewardTier _tier;

    public uint RewardCost
    {
        get { return _rewardCost; }
        set { _rewardCost = value; }
    }

    public RewardTier RewardTier
    {
        get { return _tier; }
        set { _tier = value; }
    }


    void Awake()
    {
        uiController = GetComponent<RewardUIController>();
    }


    void Start()
    {
        // Get reference to the UI Controller
        // uiController = GetComponent<TaskUIController>();
        if (uiController == null)
        {
            Debug.LogError("RewardUIController is not attached to the Task GameObject!", this);
            return;
        }

        // Subscribe to the activate button's event
        uiController.activateButton.onClick.AddListener(TriggerOnActivate);
        uiController.editButton.onClick.AddListener(TriggerOnInitEdit);
    }


    // Initialization method for dynamic setup
    public void Initialize(Dictionary<string, string> keyValuePairs)
    {
        foreach (var pair in keyValuePairs)
        {
            string key = pair.Key;

            if (key.Contains("Name"))
            {
                ItemName = pair.Value;
                continue;
            }

            if (key.Contains("Description"))
            {
                ItemDescription = pair.Value;
                continue;
            }

            if (key.Contains("Cost"))
            {
                if (uint.TryParse(pair.Value, out uint result))
                {
                    RewardCost = result;
                }
                continue;
            }

            if (key.Contains("Tier"))
            {
                if (Enum.TryParse(pair.Value, out RewardTier tier))
                {
                    RewardTier = tier;
                }
                continue;
            }
        }
    }

    public override void TriggerOnDelete()
    {
        OnDeleteReward?.Invoke(this, gameObject);
    }

    public override void TriggerOnActivate()
    {
        OnActivateReward?.Invoke(this);
    }

    public override void TriggerOnEdit()
    {
        OnEditReward?.Invoke(this);
    }

    public override void TriggerOnInitEdit()
    {
        OnInitEditReward?.Invoke(this);
    }

    public override void SetData(RewardData data)
    {
        ItemName = data.itemName;
        ItemDescription = data.itemDescription;
        RewardTier = data.tier;
        RewardCost = (uint)data.coinCost;
    }


    public override RewardData GetData()
    {
        return new RewardData {
            itemName = ItemName,
            itemDescription = ItemDescription,
            tier = RewardTier,
            coinCost = (int)RewardCost
        };
    }
}