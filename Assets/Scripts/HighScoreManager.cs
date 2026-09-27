using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public float score = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerController>() == null)
        {
            return;
        }

        score += 1;
    }
}
