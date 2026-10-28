using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelectScreen;
    public GameObject creditsScreen;
    public GameObject settingsMenu;
    public GameObject loreScreen;

    public GameObject levelTwoLock;
    public GameObject levelThreeLock;
    public GameObject endlessLock;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        mainMenu.SetActive(true);
        levelSelectScreen.SetActive(false);
        creditsScreen.SetActive(false);
        settingsMenu.SetActive(false);
        loreScreen.SetActive(false);

        UpdateLevelLocks();
    }

    private void OnEnable()
    {
        // Refresh progression whenever this object is enabled
        if (DataManager.Instance != null)
        {
            UpdateLevelLocks();
        }
    }

    public void UpdateLevelLocks()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager not found.");
            return;
        }

        // Lock levels
        levelTwoLock.SetActive(!DataManager.Instance.IsLevelUnlocked(DataManager.LevelIndex.LevelTwo));
        levelThreeLock.SetActive(!DataManager.Instance.IsLevelUnlocked(DataManager.LevelIndex.LevelThree));
        endlessLock.SetActive(!DataManager.Instance.IsLevelUnlocked(DataManager.LevelIndex.Endless));

    }

    // Now Loads the level with the highest unlocked index
    public void PlayGame()
    {
        if (DataManager.Instance == null || DataManager.Instance.saveData == null)
        {
            Debug.LogError("DataManager or SaveData not found");
            return;
        }
        
        int level = DataManager.Instance.saveData.highestLevelUnlocked;

        switch (level)
        {
            case 1: PlayLevelOne();
                break;
            case 2: PlayLevelTwo();
                break;
            case 3: PlayLevelThree();
                break;
            case 4: PlayEndless();
                break;
            default: PlayLevelOne();
                break;
        }
    }

    private void LoadLevelIfUnlocked(DataManager.LevelIndex level,string sceneName)
    {
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager was not found");
            return;
        }

        if (!DataManager.Instance.IsLevelUnlocked(level))
        {
            Debug.Log($"{level} is locked");
            UpdateLevelLocks();
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void PlayEndless()
    {
        LoadLevelIfUnlocked(DataManager.LevelIndex.Endless, "Endless");
    }

    public void GoToLevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelectScreen.SetActive(true);
        creditsScreen.SetActive(false);
        settingsMenu.SetActive(false);
        loreScreen.SetActive(false);

        UpdateLevelLocks();
    }

    public void PlayLevelOne()
    {
        LoadLevelIfUnlocked(DataManager.LevelIndex.LevelOne, "Level 1");
    }

    public void PlayLevelTwo()
    {
        LoadLevelIfUnlocked(DataManager.LevelIndex.LevelTwo, "Level 2");
    }

    public void PlayLevelThree()
    {
        LoadLevelIfUnlocked(DataManager.LevelIndex.LevelThree, "Level 3");
    }

    public void GoToCredits()
    {
        mainMenu.SetActive(false);
        levelSelectScreen.SetActive(false);
        creditsScreen.SetActive(true);
        settingsMenu.SetActive(false);
        loreScreen.SetActive(false);
    }

    public void GoToSettings()
    {
        mainMenu.SetActive(false);
        levelSelectScreen.SetActive(false);
        creditsScreen.SetActive(false);
        settingsMenu.SetActive(true);
        loreScreen.SetActive(false);
    }

    public void GoToLore()
    {
        mainMenu.SetActive(false);
        levelSelectScreen.SetActive(false);
        creditsScreen.SetActive(false);
        settingsMenu.SetActive(false);
        loreScreen.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        levelSelectScreen.SetActive(false);
        creditsScreen.SetActive(false);
        settingsMenu.SetActive(false);
        loreScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
