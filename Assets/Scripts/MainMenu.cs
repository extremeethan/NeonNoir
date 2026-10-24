using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelectScreen;
    public GameObject creditsScreen;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        mainMenu.SetActive(true);
        levelSelectScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }

    // Loads the next scene listed in the Build Index
    // Needs to be changed with progression in mind
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayEndless()
    {
        SceneManager.LoadScene("Endless");
    }

    public void LevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelectScreen.SetActive(true);
        creditsScreen.SetActive(false);
    }

    public void PlayLevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void PlayLevelTwo()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void PlayLevelThree()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        levelSelectScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        levelSelectScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
