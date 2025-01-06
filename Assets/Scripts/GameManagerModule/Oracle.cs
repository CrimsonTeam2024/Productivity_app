using TMPro;
using UnityEngine;

public class Oracle : MonoBehaviour
{
    public float oracleSatisfaction;
    [SerializeField] TMP_Text tasksCompletedText;
    [SerializeField] TMP_Text focusTimeText;
    [SerializeField] TMP_Text oracleSatisfactionText;
    int lastTasksCompleted;

    void Awake()
    {
        oracleSatisfaction = 1f;
    }

    void Start()
    {
        UpdateOracleStats();
    }

    void Update()
    {
        if (GameManager.Instance.tasksCompleted != lastTasksCompleted)
        {
            lastTasksCompleted = (int)GameManager.Instance.tasksCompleted;
            UpdateOracleSatisfaction();
        }
        else
        {
            oracleSatisfaction = 1f;
        }

        UpdateOracleStats();   
    }

    public void UpdateOracleStats()
    {
        tasksCompletedText.text = GameManager.Instance.tasksCompleted.ToString();
        focusTimeText.text = GameManager.Instance.focusTime.ToString();
        oracleSatisfactionText.text = oracleSatisfaction.ToString("P0");
    }

    public void UpdateOracleSatisfaction()
    {
        
    }
}