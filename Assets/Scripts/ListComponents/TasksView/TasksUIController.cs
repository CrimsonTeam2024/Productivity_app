using UnityEngine;
using UnityEngine.Scripting;

public class TasksUIController : ListUIController<Task>
{
    private GameObject newTaskButton;
    private GameObject taskList;

    protected override void Awake()
    {
        base.Awake();
        newTaskButton = GameObject.Find("NewTaskButton");
        taskList = GameObject.Find("TaskList");
    }

    public void ShowTaskScene()
    {
        // Show task scene
        taskList.SetActive(true);
        newTaskButton.SetActive(true);
    }

    public void HideTaskScene()
    {
        // Hide task scene
        taskList.SetActive(false);
        newTaskButton.SetActive(false);
    }
}