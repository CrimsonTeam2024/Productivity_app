using UnityEngine;



public class ListItem : MonoBehaviour
{
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


    public void Delete()
    {
        Destroy(gameObject);
    }
}