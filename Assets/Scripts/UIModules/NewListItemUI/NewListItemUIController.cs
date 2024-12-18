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

    public abstract T GetListItemFromUI();
}
