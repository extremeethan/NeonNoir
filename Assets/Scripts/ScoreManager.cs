using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float score = 0f;
    public float scoreRate = 10f;
    public Text scoreText;

    public GameManager gameManager;
    public PauseMenu pauseMenu;
    public DataManager.LevelIndex currentLevel;
    
    void Update()
    {
        if (FindObjectOfType<PlayerController>() == null || !gameManager.isGameActive || pauseMenu.isPaused)
        {
            return;
        }

        score += scoreRate * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();
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
}
