using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public double xp;
    public uint coins;

    // Stats for oracle
    public uint tasksCompleted;
    public uint rewardsCompleted;
    public uint focusTime;


    public TextMeshProUGUI coinText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Subscribe to Application Quit event
        Application.quitting += SaveGameData;

        // Initialize Player Data
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            LoadGameData();
        }
        else
        {
            coins = 650;
            xp = 0;
        }
    }

    void Start()
    {
        // Once the UI is ready, update the display
        UpdateCoinDisplay();
    }

    // Scene switching examples
    public void LoadTaskScene() => SceneManager.LoadScene("TaskScene");
    public void LoadRewardScene() => SceneManager.LoadScene("RewardScene");
    public void LoadVillageScene() => SceneManager.LoadScene("VillageScene");
    public void LoadUpgradeScene() => SceneManager.LoadScene("UpgradeScene");

    // Saving/Loading PlayerPrefs
    public void SaveGameData()
    {
        PlayerData data = new PlayerData
        {
            Coins = coins,
            Xp = xp
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("PlayerData", json);
        Debug.Log("Game data saved");
    }

    public void LoadGameData()
    {
        try
        {
            string json = PlayerPrefs.GetString("PlayerData");
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            coins = data.Coins;
            xp = data.Xp;
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading game data: " + e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    public void OnDestroy()
    {
        // Unsubscribe
        Application.quitting -= SaveGameData;
    }

    public bool SpendCoins(uint amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateCoinDisplay();
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough coins");
            return false;
        }
    }

    public void UpdateCoinDisplay()
    {
        if (coinText != null)
        {
            coinText.text = $"{coins}";
        }
    }

    public void AddCoins(uint amount)
    {
        coins += amount;
        UpdateCoinDisplay(); // Update coin count as displayed in UI
    }


    void UpdateXP(double amount)
    {
        xp += amount;
    }


    void UpdateCoins(int amount)
    {
        coins += (uint)amount;
        UpdateCoinDisplay();
    }


    public void UpdateStats(Task completedTask)
    {
        // Update XP
        double xpChange = CalculateXpChange(completedTask);
        UpdateXP(xpChange);

        // Update coins
        int coinChange = CalculateCoinChangeOnTaskCompletion(completedTask);
        UpdateCoins(coinChange);

        focusTime += completedTask.TimeCost;
        tasksCompleted++;
    }


    public void UpdateStats(Reward completedReward)
    {
        // Update XP
        double xpChange = CalculateXpChange(completedReward);
        UpdateXP(xpChange);

        // Update coins
        int coinChange = -(int)completedReward.RewardCost;
        UpdateCoins(coinChange);
    }


    double CalculateXpChange(Task completedTask)
    {
        TaskTier taskTier = completedTask.TaskTier;
        switch (taskTier)
        {
            case TaskTier.Easy:
                return 10;
            case TaskTier.Medium:
                return 20;
            case TaskTier.Hard:
                return 50;
            default: 
                return 0;
        }
    }


    double CalculateXpChange(Reward completedReward)
    {
        RewardTier rewardTier = completedReward.RewardTier;
        switch (rewardTier)
        {
            case RewardTier.Small:
                return 0;
            case RewardTier.Medium:
                return 0;
            case RewardTier.Big:
                return 0;
            default: 
                return 0;
        }
    }


    int CalculateCoinChangeOnTaskCompletion(Task completedTask)
    {
        TaskTier taskTier = completedTask.TaskTier;
        switch (taskTier)
        {
            case TaskTier.Easy:
                return 5;
            case TaskTier.Medium:
                return 10;
            case TaskTier.Hard:
                return 20;
            default:
                return 0;
        }
    }
}
