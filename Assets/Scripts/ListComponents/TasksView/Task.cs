using System;
using System.Collections.Generic;
using UnityEngine;



public class Task : ListItem<TaskData>
{
    public static event Action<Task> OnActivateTask;
    public static event Action<Task, GameObject> OnDeleteTask;
    TaskUIController uiController;

    [SerializeField] uint _timeCost = 10; // seconds
    [SerializeField] TaskTier _taskTier = TaskTier.Medium;

    public uint TimeCost 
    { 
        get { return _timeCost; } 
        set { _timeCost = value; } 
    } 

    public TaskTier TaskTier 
    { 
        get { return _taskTier; } 
        set { _taskTier = value; } 
    }


    void Awake()
    {
        uiController = GetComponent<TaskUIController>();
    }


    void Start()
    {
        // Get reference to the UI Controller
        // uiController = GetComponent<TaskUIController>();
        if (uiController == null)
        {
            Debug.LogError("TaskUIController is not attached to the Task GameObject!", this);
            return;
        }

        // Subscribe to the activate button's event
        uiController.activateButton.onClick.AddListener(TriggerOnActivate);
    }


    private void OnDestroy()
    {
        // Unsubscribe from the UI Controller event to avoid memory leaks
        if (uiController != null)
        {
            uiController.activateButton.onClick.RemoveListener(TriggerOnActivate);
        }
    }


    // Initialization method for dynamic setup
    // public void Initialize(Dictionary<string, string> keyValuePairs)
    // {
    //     foreach (var pair in keyValuePairs)
    //     {
    //         string key = pair.Key;

    //         if (key.Contains("Name"))
    //         {
    //             ItemName = pair.Value;
    //             continue;
    //         }

    //         if (key.Contains("Description"))
    //         {
    //             ItemDescription = pair.Value;
    //             continue;
    //         }

    //         if (key.Contains("Cost"))
    //         {
    //             if (uint.TryParse(pair.Value, out uint result))
    //             {
    //                 TimeCost = result;
    //             }
    //             continue;
    //         }

    //         if (key.Contains("Tier"))
    //         {
    //             if (Enum.TryParse(pair.Value, out TaskTier tier))
    //             {
    //                 TaskTier = tier;
    //             }
    //             continue;
    //         }
    //     }
    // }

    public override void SetData(TaskData taskData)
    {
        ItemName = taskData.itemName;
        ItemDescription = taskData.itemDescription;
        TaskTier = taskData.tier;
        TimeCost = (uint)taskData.timeCost;

        uiController.UpdateUIValues(this);
    }


    public override void TriggerOnDelete()
    {
        OnDeleteTask?.Invoke(this, gameObject);
    }


    public override void TriggerOnActivate()
    {
        print("Activating Task!");
        OnActivateTask?.Invoke(this);
    }
}