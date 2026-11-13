using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    public int scoreValue = 50;
    public AudioSource audioSource;
    public AudioClip MunchFood;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
            other.GetComponent<PlayerController>().RestoreLife();
            Destroy(gameObject);
        }
    }
}
