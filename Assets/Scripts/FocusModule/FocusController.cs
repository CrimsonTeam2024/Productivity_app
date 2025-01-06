using UnityEngine;

public class FocusController : MonoBehaviour
{
    public Task focusedTask;
    public Timer focusTime;
    Coroutine timerCoroutine;

    [SerializeField] FocusUIController focusUIController;
    [SerializeField] TasksController tasksController;
    GameManager gameManager;


    void Start()
    {
        gameManager = GameManager.Instance;
    }


    public void ShowFocusView()
    {
        gameObject.SetActive(true);
    }


    public void InitFocusSession(Task taskToActivate) // TODO: Connect ListController to this
    {
        ShowFocusView();
        focusUIController.UpdateFocusViewData(taskToActivate);
        focusedTask = taskToActivate;
    }

    public void StartFocusTimer() // TODO: Connect ListController to this
    {
        Task activatedTask = focusedTask;
        focusTime = new Timer(activatedTask.TimeCost);
        focusTime.OnTimerEnd += CompleteFocusSession; // Subscribes the EndFocusTimer method to the OnTimerEnd Event
        
        focusUIController.ShowFocusTimer(focusTime);
        
        // StartCoroutine(focusTime.StartClock(activatedTask));
        timerCoroutine = StartCoroutine(focusTime.TimerTickDown(activatedTask));

        // TODO: Handle updates to Village System
    }


    public void CompleteFocusSession(Task completedTask)
    {
        focusUIController.EndFocusTimer(); // Handle UI on timer End

        focusTime.OnTimerEnd -= CompleteFocusSession;

        completedTask.TriggerOnDelete();

        gameManager.UpdateStats(completedTask);
        // TODO: Hande updates to Village System
    }


    public void CancelFocusSession()
    {
        StopCoroutine(timerCoroutine);
        focusTime.OnTimerEnd -= CompleteFocusSession;
        focusTime.ResetClock();
        focusTime = null;
        focusedTask = null;
        ResetFocusUI();
        gameObject.SetActive(false);
    }


    public void ResetFocusUI()
    {
        focusUIController.ResetFocusUI();
    }
}