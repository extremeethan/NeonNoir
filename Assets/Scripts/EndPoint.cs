using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public DataManager.LevelIndex currentLevel;
    private bool levelCompleted = false;

    public string loreID;

    public VideoManager outroVideoManager;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true;

            if (outroVideoManager != null)
            {
                outroVideoManager.PlayOutro(CompleteLevel);
            }
            else
            {
                CompleteLevel();
            }
        }
    }

    void CompleteLevel()
    {
        ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();

        if (scoreManager != null)
        {
            DataManager.Instance.SaveHighestScore(currentLevel, Mathf.FloorToInt(scoreManager.score));
        }

        if (!string.IsNullOrEmpty(loreID))
        {
            DataManager.Instance.CollectLore(loreID);
        }

        DataManager.Instance.UnlockedNextLevel(currentLevel);
        FindFirstObjectByType<GameManager>().LevelComplete();
        Debug.Log($"{currentLevel} complete.");
    }
}
