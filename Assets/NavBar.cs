using UnityEngine;

public class NavBar : MonoBehaviour
{
    public GameObject viewsParent;
    [SerializeField] int currentViewIndex;
    

    public void SetCurrentViewIndex(int value)
    {
        if (value >= 0 && value < 4)
        {
            currentViewIndex = value;
            int viewPositionShift = currentViewIndex * -1080;
            viewsParent.GetComponent<RectTransform>().localPosition = new Vector3(viewPositionShift, 0, 0);
        }
        else
        {
            Debug.LogError("CurrentViewIndex must not be less than 0 or greater than 3.");
        }
    }
}
