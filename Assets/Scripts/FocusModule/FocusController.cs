using UnityEngine;

public class FocusController : MonoBehaviour
{
    public Task focusedTask;
    public Timer focusTime;
    [SerializeField] FocusUIController focusUIController;
    [SerializeField] TasksController tasksController;


    public void ShowFocusView()
    {
        gameObject.SetActive(true);
    }


    public void InitFocusSession(Task taskToActivate) // TODO: Connect ListController to this
    {
        ShowFocusView();
        focusedTask = taskToActivate;
    }


    public void StartFocusTimer() // TODO: Connect ListController to this
    {
        Task activatedTask = focusedTask;
        focusTime = new Timer(activatedTask.TimeCost);
        focusTime.OnTimerEnd += EndFocusTimer; // Subscribes the EndFocusTimer method to the OnTimerEnd Event
        
        focusUIController.FakeShowFocusTimer(focusTime);
        
        StartCoroutine(focusTime.StartClock(activatedTask));

        // TODO: Handle updates to Village System
    }
    

    public void EndFocusTimer(Task completedTask)
    {
        focusUIController.EndFocusTimer(); // Handle UI on timer End

        focusTime.OnTimerEnd -= EndFocusTimer;

        completedTask.TriggerOnDelete();

        // TODO: Hande updates to Village System

    }
}