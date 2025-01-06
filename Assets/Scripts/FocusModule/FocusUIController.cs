using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FocusUIController : MonoBehaviour
{
    
    // TODO: Consider whether there is a better way of getting focusTimerText, 
    //       than defining it in the Unity inspector
    [SerializeField] TMP_Text focusTimerText;
    [SerializeField] GameObject hammerIcon;
    [SerializeField] GameObject onwardPopup;
    [SerializeField] GameObject cancelPopup;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject stopButton;
    [SerializeField] Image focusRingToFill;
    [SerializeField] TMP_Text taskTitle;
    [SerializeField] TMP_Text taskDescription;
    GameObject focusViewObject;
    Timer _timer;
    bool isTimerShown;
    float timerCompletion;
    float timeFromStart;


    void Awake()
    {
        focusViewObject = this.gameObject;
    }

    
    public void ShowFocusTimer(Timer time)
    {
        _timer = time;
        if (focusViewObject != null)
        {
            focusTimerText.gameObject.SetActive(true);
            hammerIcon.SetActive(false);

            focusTimerText.text = _timer.ToString();
            _timer.OnTimerTick += UpdateTimer; // subscribe to the event to update the text
        }
    }


    public void UpdateFocusViewData(Task task)
    {
        taskTitle.text = task.ItemName;
        taskDescription.text = task.ItemDescription;
    }


    void Update()
    {
        if (_timer != null)
        {
            timeFromStart += Time.deltaTime;
            focusRingToFill.fillAmount = timeFromStart / _timer.TotalSeconds;
        }
    }


    private void UpdateTimer(Timer time)
    {
        if (focusTimerText != null)
        { 
            focusTimerText.text = time.ToString();
        }

        focusRingToFill.fillAmount = time.TimerCompletion; 
    }

    // Method is triggered when OnTimerEnd event triggers
    public void EndFocusTimer()
    {
        // hide the timer
        if (focusViewObject != null)
        {
            isTimerShown = false;
            focusTimerText.gameObject.SetActive(false);
            onwardPopup.SetActive(true);
            YesNoPopUpBox popopDynamics = onwardPopup.GetComponent<YesNoPopUpBox>();
            popopDynamics.ShowBanner();
            stopButton.SetActive(false);
            backButton.SetActive(false);
            cancelPopup.SetActive(false);
        }
    }


    public void ResetFocusUI()
    {
        focusTimerText.text = "00:00:00";
        hammerIcon.SetActive(true);
        onwardPopup.SetActive(false);
        cancelPopup.SetActive(false);
        backButton.SetActive(true);
        stopButton.SetActive(false);
        focusTimerText.gameObject.SetActive(false);
        timeFromStart = 0;
        focusRingToFill.fillAmount = 0;
        _timer = null;
    }
}