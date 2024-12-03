using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: This needs to be expanded to include the oracle system
public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set;}

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
        SceneManager.LoadScene("TaskScene");
    }

    public void LoadRewardScene() {
        SceneManager.LoadScene("RewardScene");
    }

    public void LoadVillageScene() {
        SceneManager.LoadScene("VillageScene");
    }

    public void LoadUpgradeScene() {
        SceneManager.LoadScene("UpgradeScene");
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

    public bool SpendCoins(uint amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            return true;
        }
        return false;
    }

    public void AddCoins(uint amount)
    {
        coins += amount;
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

// class for save/load
//[Serializable]
//public class PlayerData
//{
//    public uint Coins;
//    public double Xp;
//}
