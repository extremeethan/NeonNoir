using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float score = 0f;
    public Text scoreText;
    
    void Update()
    {
        if (FindObjectOfType<PlayerController>() == null)
        {
            return;
        }

        score += 1;
        scoreText.text = score.ToString();
    }
}
