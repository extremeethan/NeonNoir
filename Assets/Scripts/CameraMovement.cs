using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public PlayerController playerController;

    void Update()
    {
        // Camera move right with player speed
        transform.Translate(Vector2.right * playerController.playerSpeed * Time.deltaTime);
    }
}
