using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public SaveData saveData;

    private IDataService dataService;
    private const string SavePath = "/batsby-save.json";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        dataService = new JsonDataService();
        LoadGame();
    }

    public void LoadGame()
    {
        try
        {
            saveData = dataService.LoadData<SaveData>(SavePath, false);
        }
        catch (Exception e)
        {
            Debug.LogWarning("No valid save found. Creating new save: " + e.Message);

            saveData = new SaveData();
            SaveGame();
        }

        CollectLore("Lore_I_LevelOne");
    }

    public void SaveGame()
    {
        if (saveData == null)
            return;
        if (!dataService.SaveData(SavePath, saveData, false))
        {
            Debug.LogError("Failed to save player data.");
        }
    }

    public enum LevelIndex
    {
        LevelOne = 0,
        LevelTwo = 1,
        LevelThree = 2,
        Endless = 3
    }

    public int GetHighestScore(LevelIndex level)
    {
        switch (level)
        {
            case LevelIndex.LevelOne:
                return saveData.levelOneHighestScore;

            case LevelIndex.LevelTwo:
                return saveData.levelTwoHighestScore;

            case LevelIndex.LevelThree:
                return saveData.levelThreeHighestScore;

            case LevelIndex.Endless:
                return saveData.endlessHighestScore;

            default:
                return 0;
        }
    }

    public void SaveHighestScore(LevelIndex level, int score)
    {
        if (score <= GetHighestScore(level))
            return;

        switch (level)
        {
            case LevelIndex.LevelOne:
                saveData.levelOneHighestScore = score;
                break;

            case LevelIndex.LevelTwo:
                saveData.levelTwoHighestScore = score;
                break;

            case LevelIndex.LevelThree:
                saveData.levelThreeHighestScore = score;
                break;

            case LevelIndex.Endless:
                saveData.endlessHighestScore = score;
                break;
        }
        
        SaveGame();
    }

    public bool IsLevelUnlocked(LevelIndex level)
    {
        return (int)level + 1 <= saveData.highestLevelUnlocked;
    }

    public void UnlockedNextLevel(LevelIndex completedLevel)
    {
        int nextLevel = (int)completedLevel + 2;

        if (nextLevel > 4)
            return;

        if (nextLevel > saveData.highestLevelUnlocked)
        {
            saveData.highestLevelUnlocked = nextLevel;

            // pre
            switch (nextLevel)
            {
                case 2:
                    CollectLore("Lore_I_LevelTwo");
                    break;
                case 3:
                    CollectLore("Lore_I_LevelThree");
                    break;
            }
            SaveGame();
        }
    }

    public bool HasCollectedLore(string loreID)
    {
        return saveData.collectedLoreIDs.Contains(loreID);
    }

    public void CollectLore(string loreID)
    {
        if (HasCollectedLore(loreID))
            return;

        saveData.collectedLoreIDs.Add(loreID);
        SaveGame();
    }

    public void UnlockAll()
    {
        // Highest Level Unlocked is Endless
        saveData.highestLevelUnlocked = 4;

        // Unlock All Lore
        CollectLore("Lore_I_LevelOne");
        CollectLore("Lore_O_LevelOne");
        CollectLore("Lore_I_LevelTwo");
        CollectLore("Lore_O_LevelTwo");
        CollectLore("Lore_I_LevelThree");
        CollectLore("Lore_O_LevelThree");

        // Refresh Menu UI
        MainMenu mainMenu = FindFirstObjectByType<MainMenu>();

        if (mainMenu != null)
        {
            mainMenu.UpdateLevelLocks();
            mainMenu.UpdateLoreUnlocks();
        }

        // Save
        SaveGame();

        // Console
        Debug.Log("Master Key Inserted. Everything is Unlocked!");
    }
}
