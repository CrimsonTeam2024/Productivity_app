using System;
using UnityEngine;

// TODO: This needs to be expanded to include the oracle system
public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set;}
    public GameObject TaskListView;
    // public GameObject RewardView;
    // public GameObject VillageView;
    // public GameObject UpgradeView;

    // TODO: Consider separate classes for the following values
    public double xp;
    public uint coins;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        }

        // Subscribe to the Application Quit event
        Application.quitting += SaveGameData;

        // Initialize Player Data
        if (PlayerPrefs.HasKey("PlayerData")) {
            LoadGameData();
        } else {
            coins = 0;
            xp = 0;
        }
    }

    // Initialize Scene Swtiching method, may be modified later
    public void LoadTaskScene() {
        if (TaskListView == null) {
            Debug.LogError("TaskListView is not set in the GameManager!");
            return;
        }
        TaskListView.SetActive(true);
        // TODO: set the other three views as deactive if any of them is active
        // RewardView.SetActive(false);
        // VillageView.SetActive(false);
        // UpgradeView.SetActive(false);
    }

    public void LoadRewardScene() {
        // RewardView.SetActive(true);
        // TaskListView.SetActive(false);
        // VillageView.SetActive(false);
        // UpgradeView.SetActive(false);
    }

    public void LoadVillageScene() {
        // VillageView.SetActive(true);
        TaskListView.SetActive(false);
        // RewardView.SetActive(false);
        // UpgradeView.SetActive(false);
    }

    public void LoadUpgradeScene() {
        // UpgradeView.SetActive(true);
        // TaskListView.SetActive(false);
        // RewardView.SetActive(false);
        // VillageView.SetActive(false);
    }

    public void SaveGameData() {
        PlayerData data = new PlayerData {
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
        // Save the game data when the application is being closed
        SaveGameData();
    }

    public void OnDestory()
    {
        Application.quitting -= SaveGameData;
    }
}
