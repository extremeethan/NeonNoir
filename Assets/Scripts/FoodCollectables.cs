using UnityEngine;

public class FoodCollectables : MonoBehaviour
{
    public int scoreValue = 10;

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ScoreManager>().AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
