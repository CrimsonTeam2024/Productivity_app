using System;
using Unity.VisualScripting;
using UnityEngine;



public abstract class ListItem<T> : MonoBehaviour where T : ListItemData
{
    // Static event that passes the ListItem and its associated GameObject
    public static event Action<ListItem<T>, GameObject> OnDeleteFromList;
    public RectTransform rectTransform;
    // protected ListItemUIController<ListItem<T>, T> uiController;

    [SerializeField] uint _index;
    public uint Index 
    {
        get { return _index; }
        set { _index = value; }
    }

    [SerializeField] string _itemName;
    public string ItemName { get { return _itemName; } set { _itemName = value; } }

    [SerializeField] string _itemDescription;
    public string ItemDescription { get { return _itemDescription; } set { _itemDescription = value; } }

    [SerializeField] Vector2 _targetPosition;
    public Vector2 TargetPosition { get { return _targetPosition; } set { _targetPosition = value; } }


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        // uiController = GetComponent<ListItemUIController<ListItem<T>, T>>();
    }


    void Update()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, TargetPosition, 0.1f);
    }


    public abstract void TriggerOnDelete();
    public abstract void TriggerOnActivate();
    public abstract void TriggerOnEdit();
    public abstract void TriggerOnInitEdit();

    public abstract void TriggerOnComplete();


    public void UpdateTargetPosition(float topPadding, float itemPadding)
    {
        TargetPosition = Vector2.down * (topPadding + Index * (rectTransform.sizeDelta.y + itemPadding));
    }


    public void SnapToTargetPosition()
    {
        rectTransform.transform.position = new Vector3(TargetPosition.x, TargetPosition.y, 0f);
    }


    public abstract void SetData(T data);
    public abstract T GetData();
}