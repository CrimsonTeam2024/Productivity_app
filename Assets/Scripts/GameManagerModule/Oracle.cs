using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Oracle : MonoBehaviour
{
    public float oracleSatisfaction;
    [SerializeField] TMP_Text tasksCompletedText;
    [SerializeField] TMP_Text focusTimeText;
    [SerializeField] TMP_Text oracleSatisfactionText;
    [SerializeField] List<Button> oracleMessageButtons;
    int lastTasksCompleted;
    PopUpMessage popUpMessage;

    List<string>[] messagePools = {
        new List<string> { 
            "You are not doing well", 
            "You need to improve", 
            "Things are looking bleak", 
            "This is not good"
        }, // 0-25% satisfaction

        new List<string> { 
            "You are doing okay", 
            "Keep going", 
            "There's room for improvement", 
            "You can do better"
        }, // 26-50% satisfaction

        new List<string> { 
            "You are doing well", 
            "Good job", 
            "Keep up the good work", 
            "Nice progress"
        }, // 51-75% satisfaction

        new List<string> { 
            "You are doing great", 
            "Excellent work", 
            "You are on top of things", 
            "Outstanding performance"
        } // 76-100% satisfaction
    };

    void Awake()
    {
        oracleSatisfaction = 1f;
    }

    void Start()
    {
        UpdateOracleStats();
        UpdateOracleMessages(oracleSatisfaction);
    }

    void Update()
    {
        UpdateOracleStats();
        UpdateOracleMessages(oracleSatisfaction);
    }

    public void UpdateOracleStats()
    {
        tasksCompletedText.text = GameManager.Instance.tasksCompleted.ToString();
        uint focusSeconds = GameManager.Instance.focusTime;
        focusTimeText.text = new Timer(focusSeconds).ToString(true);
        oracleSatisfactionText.text = oracleSatisfaction.ToString("P0");
    }

    public void UpdateOracleMessages(float satisfaction)
    {
        List<string> selectedMessagePool;

        if (satisfaction < 0.25f)
        {
            selectedMessagePool = messagePools[0];
        }
        else if (satisfaction < 0.50f)
        {
            selectedMessagePool = messagePools[1];
        }
        else if (satisfaction < 0.75f)
        {
            selectedMessagePool = messagePools[2];
        }
        else
        {
            selectedMessagePool = messagePools[3];
        }

        int i = 0;
        foreach (Button msgButton in oracleMessageButtons)
        {
            popUpMessage = msgButton.gameObject.GetComponent<PopUpMessage>();
            string message = selectedMessagePool[i];
            msgButton.onClick.RemoveAllListeners();
            msgButton.onClick.AddListener(() => popUpMessage.ShowPopup(message));
            i++;
        }
    }
}