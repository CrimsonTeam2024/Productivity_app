using UnityEngine;

// Manager class attached to some manager GameObject that manages 
public abstract class ListUIController : MonoBehaviour
{
    public GameObject listItemDetailsPanel;
    public GameObject newListItemPanel;

    void Awake() {
        // Hide the action specific ui panels
        listItemDetailsPanel.SetActive(false);
        newListItemPanel.SetActive(false);
    }


    public void ShowDetailsPanel(BaseListItem listItem)
    {
        Transform listItemTransform = listItem.GetComponentInParent<Transform>();
        // TODO: Manipulate transform position such that details panel is well positioned
        
        listItemDetailsPanel.SetActive(true);
        // TODO: After instantiation, animate the reward details screen pop up
    }


    // Method to hide details
    public void HideDetails()
    {
        if (listItemDetailsPanel != null)
        {
            Destroy(listItemDetailsPanel);
        }
    }


    public void ShowCreateListItemPanel()
    {
        newListItemPanel.SetActive(true);
        // TODO: After instantiation, animate the reward details screen pop up
    }


    public void HideCreateListItemPanel()
    {
        newListItemPanel.SetActive(false);
        // TODO: After instantiation, animate the reward details screen pop up
    }
    

    public abstract void FillDetails(BaseListItem listItem);
}