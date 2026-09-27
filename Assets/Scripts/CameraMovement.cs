using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    public PlayerController playerController;

    void Update()
    {
        // Don't move camera if game isn't active
        if (FindObjectOfType<GameManager>().isGameActive == false)
        {
            return;
        }
        // Camera move right with player speed
        transform.Translate(Vector2.right * playerController.playerSpeed * Time.deltaTime);
    }
}
