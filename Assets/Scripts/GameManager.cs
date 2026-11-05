using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject HUD;

    void Start()
    {
        gameOverScreen.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
        HUD.SetActive(false);

        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.SaveHighestScore();
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LevelComplete()
    {
        if (!isGameActive)
            return;

        isGameActive = false;
        winScreen.SetActive(true);
        HUD.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
