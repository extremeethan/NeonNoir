using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public DataManager.LevelIndex currentLevel;
    private bool levelCompleted = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true;
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

            if (scoreManager != null)
            {
                DataManager.Instance.SaveHighestScore(currentLevel, Mathf.FloorToInt(scoreManager.score));
            }

            DataManager.Instance.UnlockedNextLevel(currentLevel);
            FindObjectOfType<GameManager>().LevelComplete();
            Debug.Log($"{currentLevel} complete.");
        }
    }
}
