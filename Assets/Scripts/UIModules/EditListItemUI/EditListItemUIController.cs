using TMPro;
using UnityEngine;
using UnityEngine.UI;



public abstract class EditListItemPanelUIController<T> : MonoBehaviour where T : ListItemData
{
    public TMP_InputField itemTitle;
    public TMP_InputField itemDescription;
    public TMP_Dropdown itemTier;
    public Button saveButton, deleteButton, cancelButton;

    public abstract T GetListItemFromUI();
    public abstract void UpdateUIValues(T taskData);
}
