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
    [SerializeField] GameObject endPopup;
    [SerializeField] Image focusRingToFill;
    GameObject focusViewObject;
    Timer _timer;
    bool isTimerShown;
    float timerCompletion;
    float timeFromStart;


    void Awake()
    {
        focusViewObject = this.gameObject;
    }

    
    public void FakeShowFocusTimer(Timer time)
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

    
    public void ShowFocusTimer(Timer time)
    {
        _timer = time;
        if (focusViewObject != null)
        {
            focusViewObject.SetActive(true);
            isTimerShown = true;
            focusTimerText.text = _timer.ToString();
            // _timer.OnTimerTick += UpdateTimer; // subscribe to the event to update the text
        }
    }


    // TODO: Consider a better way of doing this.
    //       Maybe we can create tick events, 
    //       rather than updating this logic once every frame
    //       even though we know we will need to update this string 
    //       only once every second. The Update method executes every frame, 
    //       so potentially hundreds of times a second. This is inefficient.
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
            endPopup.SetActive(true);
            YesNoPopUpBox popopDynamics = endPopup.GetComponent<YesNoPopUpBox>();
            popopDynamics.ShowBanner();
        }
    }
}