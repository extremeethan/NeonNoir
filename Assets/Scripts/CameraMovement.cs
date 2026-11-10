using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public PlayerController playerController;
    public float smoothTime = 0.15f;

    private float offsetX;
    private float velocityX;

    void Start()
    {
        if (playerController != null)
        {
            offsetX = transform.position.x - playerController.transform.position.x;
        }
    }

    void LateUpdate()
    {
        // Don't move camera if game isn't active
        if (FindFirstObjectByType<GameManager>().isGameActive == false || playerController == null)
        {
            return;
        }

        // Player's X position
        float targetX = playerController.transform.position.x + offsetX;

        // Smooth Camera Movement
        float newX = Mathf.SmoothDamp(transform.position.x, targetX, ref velocityX, smoothTime);

        // Set Camera to follow
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
