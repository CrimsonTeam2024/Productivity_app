using System;
using System.Collections.Generic;
using UnityEngine;

// Manager class attached to some manager GameObject that manages 
public abstract class ListUIController : MonoBehaviour
{
    public GameObject detailsPrefab; // List item details GameObject "prefab" or template
    public GameObject newListItemPrefab;
    GameObject listItemDetailsPanel; // The actual instantiated list item details GameObject
    GameObject newListItemPanel;


    public void ShowDetailsPanel(BaseListItem listItem)
    {
        Transform listItemTransform = listItem.GetComponentInParent<Transform>();
        // TODO: Manipulate transform position such that details panel is well positioned
        
        listItemDetailsPanel = Instantiate(detailsPrefab, listItemTransform);
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
        newListItemPanel = Instantiate(newListItemPrefab);
        // TODO: After instantiation, animate the reward details screen pop up
    }
    

    public abstract void FillDetails(BaseListItem listItem);
}