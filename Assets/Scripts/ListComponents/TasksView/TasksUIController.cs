using UnityEngine;
using UnityEngine.Scripting;

public class TasksUIController : ListUIController<Task>
{
    private GameObject taskList;

    protected override void Awake()
    {
        base.Awake();
        taskList = GameObject.Find("TaskList");
    }

    public void ShowTaskScene()
    {
        // Show task scene
        taskList.SetActive(true);
        newListItemButton.SetActive(true);
    }

    public void HideTaskScene()
    {
        // Hide task scene
        taskList.SetActive(false);
        newListItemButton.SetActive(false);
    }
}