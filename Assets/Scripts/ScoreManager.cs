using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float score = 0f;
    public Text scoreText;

    public GameManager gameManager;
    public PauseMenu pauseMenu;
    
    void Update()
    {
        if (FindObjectOfType<PlayerController>() == null || !gameManager.isGameActive || pauseMenu.isPaused)
        {
            return;
        }

        score += 1;
        scoreText.text = score.ToString();
    }

    public void AddScore(float points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
