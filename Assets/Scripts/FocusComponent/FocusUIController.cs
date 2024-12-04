using TMPro;
using UnityEngine;

public class FocusUIController : MonoBehaviour
{
    public GameObject focusTimerObject;
    
    // TODO: Consider whether there is a better way of getting focusTimerText, 
    //       than defining it in the Unity inspector
    [SerializeField] TMP_Text focusTimerText;
    Timer _timer;
    bool isTimerShown;

    
    public void ShowFocusTimer(Timer time)
    {
        _timer = time;
        if (focusTimerObject != null)
        {
            focusTimerObject.SetActive(true);
            isTimerShown = true;
            focusTimerText.text = _timer.ToString();
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
        if (focusTimerText != null && isTimerShown)
        {
            focusTimerText.text = _timer.ToString();
        }
    }

    // Method is triggered when OnTimerEnd event triggers
    public void EndFocusTimer()
    {
        // hide the timer
        if (focusTimerObject != null)
        {
            focusTimerObject.SetActive(false);
            isTimerShown = false;
        }
    }
}