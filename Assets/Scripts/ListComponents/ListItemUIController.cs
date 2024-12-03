using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental; // Required for Button component



public abstract class ListItemUIController<T, U> : MonoBehaviour where T : ListItem<U> where U : ListItemData
{
    [SerializeField] TMP_Text titleField;
    [SerializeField] TMP_Text descriptionField;
    [SerializeField] TMP_Text tierField;
    public Button activateButton; // Assign this in the Unity Editor

    public string TitleField { get { return titleField.text; } protected set { titleField.text = value; } }
    public string DescriptionField { get { return descriptionField.text; } protected set { descriptionField.text = value; } }
    public int TierField
    {
        get 
        {
            int result;
            int.TryParse(tierField.text, out result);
            return result;
        }
        protected set
        {
            if (value < 0 || value > 2)
                throw new ArgumentOutOfRangeException(nameof(value), "Tier must be 0, 1, or 2.");

            tierField.text = (value + 1).ToString(); // Set the dropdown's 0-based index.
        }
    }


    public abstract void UpdateUIValues(T listItem);
}
