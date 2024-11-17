using System;
using System.Collections.Generic;
using UnityEngine;

// Manager class attached to some manager GameObject that manages 
public abstract class ListItemUIController : MonoBehaviour
{
    public GameObject detailsPrefab; // List item details GameObject "prefab" or template
    GameObject listItemDetails; // The actual instantiated list item details GameObject


    public void ShowDetailsPanel(BaseListItem listItem)
    {
        Transform listItemTransform = listItem.GetComponentInParent<Transform>();
        // TODO: Manipulate transform position such that details panel is well positioned
        
        listItemDetails = Instantiate(detailsPrefab, listItemTransform);
        // TODO: After instantiation, animate the reward details screen pop up
    }


    // Method to hide details
    public void HideDetails()
    {
        if (listItemDetails != null)
        {
            Destroy(listItemDetails);
        }
    }
    

    public abstract void FillDetails(BaseListItem listItem);
}