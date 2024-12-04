using System;
using UnityEngine;



public class Task : ListItem<TaskData>
{
    public event Action<Task> OnActivateTask;
    public event Action<Task> OnEditTask;
    public event Action<Task> OnInitEditTask;
    public event Action<Task, GameObject> OnDeleteTask;
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
        uiController.editButton.onClick.AddListener(TriggerOnInitEdit);
    }


    private void OnDestroy()
    {
        // Unsubscribe from the UI Controller event to avoid memory leaks
        if (uiController != null)
        {
            uiController.activateButton.onClick.RemoveListener(TriggerOnActivate);
        }
    }


    public override void SetData(TaskData taskData)
    {
        ItemName = taskData.itemName;
        ItemDescription = taskData.itemDescription;
        TaskTier = taskData.tier;
        TimeCost = (uint)taskData.timeCost;

        uiController.UpdateUIValues(this);
    }


    public override TaskData GetData()
    {
        return new TaskData {
            itemName = ItemName,
            itemDescription = ItemDescription,
            tier = TaskTier,
            timeCost = (int)TimeCost
        };
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


    public override void TriggerOnEdit()
    {
        print("Edited task.");
        OnEditTask?.Invoke(this);
    }


    public override void TriggerOnInitEdit()
    {
        print("Editing task.");
        OnInitEditTask?.Invoke(this);
    }
}