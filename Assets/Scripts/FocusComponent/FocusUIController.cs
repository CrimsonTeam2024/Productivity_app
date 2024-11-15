using UnityEngine;
using UnityEngine.AI;

public class FocusUIController : MonoBehaviour
{
    public GameObject focusTimerPrefab;
    public Transform focusTimerTransform;
    GameObject focusGameobject;
    FocusSession focusSession;
    
    public void StartFocusTimer()
    {
        focusGameobject = Instantiate(focusTimerPrefab);
        focusSession = focusGameobject.GetComponent<FocusSession>();
        focusSession.StartFocusSession();
    }

    public void EndFocusTimer()
    {
        if (focusGameobject != null)
        {
            Destroy(focusGameobject);
        }

        if (focusSession != null)
        {
            focusSession.EndFocusSession();
            Destroy(focusSession);
        }
    }
}