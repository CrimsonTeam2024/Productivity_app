using System;
using UnityEngine;



public class ListItem : MonoBehaviour
{
    // Static event that passes the ListItem and its associated GameObject
    public static event Action<ListItem, GameObject> OnDeleteFromList;

    uint _index;
    public uint Index 
    {
        get { return _index; }
        set { _index = value; }
    }

    [SerializeField] string _itemName;
    public string ItemName { get { return _itemName; } set { _itemName = value; } }

    [SerializeField] string _itemDescription;
    public string ItemDescription { get { return _itemDescription; } set { _itemDescription = value; } }


    public void TriggerOnDelete()
    {
        // Trigger the static global delete event with this instance and its GameObject
        OnDeleteFromList?.Invoke(this, gameObject);
    }
}