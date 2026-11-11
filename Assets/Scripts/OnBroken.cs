using UnityEngine;

public class OnBroken : MonoBehaviour
{
    public int scoreValue = 50;

    public void BreakObject()
    {
        FindFirstObjectByType<ScoreManager>().AddScore(scoreValue);
        Destroy(gameObject);
    }
}
