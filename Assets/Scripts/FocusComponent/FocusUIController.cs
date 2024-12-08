using TMPro;
using UnityEngine;

public class FocusUIController : MonoBehaviour
{
    public GameObject focusTimerObject;
    public GameObject listItemPanel; // panel of list to be hidden from the inspector

    
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
            _timer.OnTimerTick += UpdateTimerText;//suscribe to the event to update the text

        }
        if (listItemPanel != null)
        {
            listItemPanel.SetActive(false);
        }
    }

    // TODO: Consider a better way of doing this.
    //       Maybe we can create tick events, 
    //       rather than updating this logic once every frame
    //       even though we know we will need to update this string 
    //       only once every second. The Update method executes every frame, 
    //       so potentially hundreds of times a second. This is inefficient.
    private void UpdateTimerText(string newTime)
    {
        if (focusTimerText != null)
        { 
            focusTimerText.text = newTime;
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


        // Show the Scroll View (listItemPanel) again
        if (listItemPanel != null)
        {
            listItemPanel.SetActive(true);
        }
    }
}