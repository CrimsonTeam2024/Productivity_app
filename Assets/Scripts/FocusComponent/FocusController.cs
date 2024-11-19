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


    public void StartFocusTimer(Task activatedTask)
    {
        focusTime = new Timer(activatedTask.taskData.TimeCost);
        focusTime.OnTimerEnd += EndFocusTimer;
        
        focusUIController.ShowFocusTimer(focusTime);
        
        // TODO: Handle updates to Village System

        StartCoroutine(focusTime.StartClock());
    }

    public void EndFocusTimer()
    {
        focusUIController.EndFocusTimer(); // Handle UI on timer End

        // TODO: Handle updates to TaskData System

        // TODO: Hande updates to Village System

    }
}