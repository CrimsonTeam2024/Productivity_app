using System;
using UnityEngine;



public abstract class ListItem : MonoBehaviour
{
    // Static event that passes the ListItem and its associated GameObject
    public static event Action<ListItem, GameObject> OnDeleteFromList;
    public RectTransform rectTransform;

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


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    void Update()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, TargetPosition, 0.1f);
    }


    public abstract void TriggerOnDelete();
    public abstract void TriggerOnActivate();


    public void UpdateTargetPosition(float topPadding, float itemPadding)
    {
        TargetPosition = Vector2.down * (topPadding + Index * (rectTransform.sizeDelta.y + itemPadding));
    }


    public void SnapToTargetPosition()
    {
        rectTransform.transform.position = new Vector3(TargetPosition.x, TargetPosition.y, 0f);
    }
}