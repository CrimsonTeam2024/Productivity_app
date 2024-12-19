using UnityEngine;

public class FocusController : MonoBehaviour
{
    public Task focusedTask;
    public Timer focusTime;
    [SerializeField] FocusUIController focusUIController;


    public void StartFocusTimer(Task activatedTask) // TODO: Connect ListController to this
    {
        focusTime = new Timer(activatedTask.TimeCost);
        focusTime.OnTimerEnd += EndFocusTimer; // Subscribes the EndFocusTimer method to the OnTimerEnd Event
        
        focusUIController.ShowFocusTimer(focusTime);
        
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