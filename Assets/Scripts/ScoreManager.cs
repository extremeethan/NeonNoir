using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float score = 0f;
    public float scoreRate = 10f;
    public Text scoreText;

    public Text currentScoreText;
    public Text highestScoreText;

    public GameManager gameManager;
    public PauseMenu pauseMenu;
    public DataManager.LevelIndex currentLevel;

    void Start()
    {
        UpdateScoreTexts();
    }

    void Update()
    {
        if (FindFirstObjectByType<PlayerController>() == null || !gameManager.isGameActive || pauseMenu.isPaused)
        {
            return;
        }

        score += scoreRate * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();

        currentScoreText.text = scoreText.text;
    }

    public void AddScore(float points)
    {
        score += points;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public void SaveHighestScore()
    {
        if (DataManager.Instance == null)
            return;

        int finalScore = Mathf.FloorToInt(score);

        DataManager.Instance.SaveHighestScore(currentLevel, finalScore);
    }

    public void UpdateScoreTexts()
    {
        if (DataManager.Instance == null)
            return;

        if (currentScoreText != null)
            currentScoreText.text = Mathf.FloorToInt(score).ToString();

        if (highestScoreText != null)
            highestScoreText.text = DataManager.Instance.GetHighestScore(currentLevel).ToString();
    }
}
