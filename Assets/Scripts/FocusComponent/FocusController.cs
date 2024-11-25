using UnityEngine;

public class FocusController : MonoBehaviour
{
    public TaskData focusedTask;
    public Timer focusTime;
    FocusUIController focusUIController;

    void Awake()
    {
        focusUIController = GetComponent<FocusUIController>();
    }


    public void StartFocusTimer(TaskData taskData)// TODO: Connect ListController to this
    {
        focusTime = new Timer(taskData.TimeCost);
        focusTime.OnTimerEnd += EndFocusTimer; // Subscribes the EndFocusTimer method to the OnTimerEnd Event
        
        focusUIController.ShowFocusTimer(focusTime);
        
        StartCoroutine(focusTime.StartClock());

        // TODO: Handle updates to Village System
    }

    public void EndFocusTimer()
    {
        focusUIController.EndFocusTimer(); // Handle UI on timer End

        // TODO: Handle updates to Task System, if there are any

        // TODO: Hande updates to Village System

    }
}