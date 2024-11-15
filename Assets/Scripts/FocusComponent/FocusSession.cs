using UnityEngine;

public class FocusSession : MonoBehaviour
{
    FocusUIController focusUI;

    public bool toggleFocus; // true mean focus active
    public float totalTime;
    public float timeRemaining;
    float timeTickResolution = 1f; // seconds


    public void StartFocusSession()
    {
        timeRemaining = totalTime;
        focusUI.StartFocusTimer();
    }

    public void EndFocusSession()
    {
        timeRemaining = 0f;
        toggleFocus = false;
    }
    

    void Update()
    {
        if (toggleFocus)
        {
            timeRemaining -= Time.deltaTime;
        }
    }
}
