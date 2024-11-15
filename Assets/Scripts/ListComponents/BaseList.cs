using System.Collections.Generic;
using UnityEngine;

public abstract class BaseList<T> : MonoBehaviour
{
    protected UIManager uiManager;
    [SerializeField] protected List<T> _items = new List<T>();

    void AddItem(T item)
    {
        _items.Add(item);
    }

    void RemoveItem(T item)
    {

    }    
}