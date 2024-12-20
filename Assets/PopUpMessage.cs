using UnityEngine;

public class PopUpMessage : MonoBehaviour
{
    [SerializeField] Transform viewTransform;
    [SerializeField] GameObject popupMessagePrefab;


    public void ShowPopup(string message)
    {
        GameObject popupObj = Instantiate(popupMessagePrefab, viewTransform);
        PopUpBox popupDynamics = popupObj.GetComponent<PopUpBox>();
        popupDynamics.ShowBanner(message);
    }
}
