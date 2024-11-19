using UnityEngine;

[System.Serializable]
public class ListItemData
{
    [SerializeField] private string _itemName;
    public string ItemName { get { return _itemName; } set { _itemName = value; } }

    [SerializeField] private string _itemDescription;
    public string ItemDescription { get { return _itemDescription; } set { _itemDescription = value; } }
}
