using TMPro;
using UnityEngine;

public class FocusUIController : MonoBehaviour
{
    public static FocusUIController Instance;

    public GameObject focusTimerObject;
    [SerializeField] TMP_Text focusTimerText;
    GameObject focusGameobject;
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

    void Update()
    {
        if (focusTimerText != null && isTimerShown)
        {
            focusTimerText.text = _timer.ToString();
        }
    }

    public void EndFocusTimer()
    {
        // TODO
    }
}