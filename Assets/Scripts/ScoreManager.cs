using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float score = 0f;
    public float scoreRate = 10f;
    public Text scoreText;

    public Text[] currentScoreTexts;
    public Text[] highestScoreTexts;

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

        UpdateScoreTexts();
    }

    public void AddScore(float points)
    {
        score += points;
        scoreText.text = Mathf.FloorToInt(score).ToString();

        UpdateScoreTexts();
    }

    public void SaveHighestScore()
    {
        if (DataManager.Instance == null)
        {
            return;
        }

        int finalScore = Mathf.FloorToInt(score);

        DataManager.Instance.SaveHighestScore(currentLevel, finalScore);

        UpdateScoreTexts();
    }

    public void UpdateScoreTexts()
    {
        if (DataManager.Instance == null)
        {
            return;
        }

        foreach (Text text in currentScoreTexts)
        {
            if (text != null)
                text.text = Mathf.FloorToInt(score).ToString();
        }

        foreach (Text text in highestScoreTexts)
        {
            if (text != null)
                text.text = DataManager.Instance.GetHighestScore(currentLevel).ToString();
        }
    }
}
