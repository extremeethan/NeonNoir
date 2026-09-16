using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float playerAcceleration = 0.1f;
    private Rigidbody2D rb;

    public float jumpHeight = 7f;
    public float diveSpeed = 14f;
    private bool isGrounded = true;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Player Speed increases over time
        playerSpeed += playerAcceleration * Time.deltaTime;
        // Player moves RIGHT endlessly
        transform.Translate (Vector2.right * playerSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
            isGrounded = false;
        }

        // Requiring the player to NOT be on the ground in order to dive, hopefully to prevent them from forcing themselves through the floor lol
        if (Input.GetKeyDown(KeyCode.S) && !isGrounded)
        {
            Dive();
        }
    }

    void Jump()
    {
        // Set player velocity to jump height 
        Vector2 velocity = rb.linearVelocity;
        velocity.y = jumpHeight;
        rb.linearVelocity = velocity;
    }

    void Dive()
    {
        // Negate player velocity so they go down
        Vector2 velocity = rb.linearVelocity;
        velocity.y = -diveSpeed;
        rb.linearVelocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Obstacle")
        {
            Debug.Log(" Ouch");
        }
        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
