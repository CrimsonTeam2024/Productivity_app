using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfoPanel : MonoBehaviour
{
    public static BuildingInfoPanel Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI healthText;

    private void Awake()
    {
        Debug.Log("BuildingUIController awake()");
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show(BuildingStats stats)
    {
        if (stats == null) return;

        // UI update
        healthText.text = $"{stats.currentHealth} / {stats.maxHealth}";

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
