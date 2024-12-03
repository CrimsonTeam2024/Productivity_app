using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public abstract class NewListItemPanelUIController<T> : MonoBehaviour where T : ListItemData
{
    public TMP_InputField itemTitle;
    public TMP_InputField itemDescription;
    public TMP_Dropdown itemTier;
    public Button saveButton, cancelButton;
    private void Start()
    {
        // Assign the SaveButtonOnClick method to the saveButton's onClick event
        // saveButton.onClick.AddListener(SaveButtonOnClick);
    }

    /// <summary>
    /// This method is called when the save button is clicked.
    /// </summary>
    // public abstract void SaveButtonOnClick();

    // protected abstract void HandleSave();

    public abstract T GetListItemFromUI();
}
