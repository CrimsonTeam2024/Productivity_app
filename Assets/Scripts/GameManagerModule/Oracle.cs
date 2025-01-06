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
        UpdateOracleStats();   
    }

    public void UpdateOracleStats()
    {
        tasksCompletedText.text = GameManager.Instance.tasksCompleted.ToString();
        uint focusSeconds = GameManager.Instance.focusTime;
        focusTimeText.text = new Timer(focusSeconds).ToString(true);
        oracleSatisfactionText.text = oracleSatisfaction.ToString("P0");
    }
}