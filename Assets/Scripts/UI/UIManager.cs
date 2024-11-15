using System;
using System.Collections.Generic;
using UnityEngine;

// Manager class attached to some manager GameObject that manages 
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    
    // The list item details ui prefabs
    public GameObject taskDetailsPrefab;
    public GameObject rewardDetailsPrefab;


    // Dictionary to map each type to its corresponding prefab
    private Dictionary<Type, GameObject> uiPrefabs;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeUIPrefabs();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Initialize the dictionary with type-prefab mappings
    private void InitializeUIPrefabs()
    {
        uiPrefabs = new Dictionary<Type, GameObject>
        {
            { typeof(Task), taskDetailsPrefab },
            { typeof(Reward), rewardDetailsPrefab }
        };
    }
    

    // Generic method to show details for a specific type
    public GameObject ShowDetails<T>(T listItem) where T : BaseListItem
    {
        Transform itemTransform = listItem.GetComponentInParent<Transform>();

        if (uiPrefabs.TryGetValue(typeof(T), out GameObject prefab))
        {
            GameObject detailsUIInstance = Instantiate(prefab, itemTransform);
            detailsUIInstance.SetActive(true);
            return detailsUIInstance;
        }
        else
        {
            Debug.LogError($"UI prefab for type {typeof(T)} not found.");
            return null;
        }
    }

    // Method to hide details
    public void HideDetails(GameObject detailsUIInstance)
    {
        // GameObject detailsUIInstance = listItem.GetComponentInChildren<GameObject>();

        if (detailsUIInstance != null)
        {
            Destroy(detailsUIInstance);
        }
    }
}