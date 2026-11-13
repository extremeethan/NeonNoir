using UnityEngine;

public class FoodCollectables : MonoBehaviour
{
    public int scoreValue = 50;
    public AudioSource audioSource;
    public AudioClip MunchFood;

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            audioSource.PlayOneShot(MunchFood);
            FindFirstObjectByType<ScoreManager>().AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
