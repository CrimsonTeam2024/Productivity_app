using UnityEngine;
public abstract class BaseListItem : MonoBehaviour
{
    string _itemName;
    public string ItemName { get { return _itemName; } set { _itemName = value; } }
    string _itemDescription;
    public string ItemDescription { get { return _itemDescription; } set { _itemDescription = value; } }
}