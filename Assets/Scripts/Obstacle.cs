using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // im setting this up in case having this kinda function in the player controller runs into issues where entering the obstacle collider accidentally takes away too much health at a time
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.LoseLife();
            }
        }
    }
}
