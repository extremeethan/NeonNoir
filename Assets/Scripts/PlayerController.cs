using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float playerAcceleration = 0.1f;
    private Rigidbody2D rb;

    public float jumpHeight = 7f;
    public float diveSpeed = 14f;
    public float stallTime = 0.5f;
    public float stallTimer = 0f;
    private bool isGrounded = true;

    public int maxLives = 3;
    public int currentLives;
    private bool isDead;

    public GameManager gameManager;

    [SerializeField] private Animator animator;

    private Vector2 touchStartPosition;
    public float swipeThreshold = 50f;

    void Start()
    {
        gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        currentLives = maxLives;
        animator = this.GetComponent<Animator>();
    }

    // Update is for events called per frame
    void Update()
    {
        // Player Speed increases over time
        playerSpeed += playerAcceleration * Time.deltaTime;

        // Player moves RIGHT endlessly
        transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
            animator.SetBool("SwipeUporRecover", true);
            isGrounded = false;
        }

        // Requiring the player to NOT be on the ground in order to dive, hopefully to prevent them from forcing themselves through the floor lol
        if (Input.GetKeyDown(KeyCode.S) && !isGrounded)
        {
            Dive();
            animator.SetBool("SwipeDown", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Stall();
            animator.SetBool("Spin", true);
        }

        // Timer for stall time
        if (stallTimer > 0)
        {
            stallTimer -= Time.deltaTime;
        }

        // Player has gone below the bottom height limit, KILL THEM
        if (transform.position.y <= -5.55f)
        {
            Die();
        }

        // Mobile Input
        if (Input.touchCount >0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 touchEndPosition = touch.position;
                Vector2 swipe = touchEndPosition - touchStartPosition;
                
                if (swipe.y > swipeThreshold && Mathf.Abs(swipe.y) > Mathf.Abs(swipe.x))
                {
                    Jump();
                    isGrounded = false;
                }
                
                else if (swipe.y < -swipeThreshold && Mathf.Abs(swipe.y) > Mathf.Abs(swipe.x))
                {
                    if (!isGrounded)
                    {
                        Dive();
                    }
                }

                else if (swipe.magnitude < swipeThreshold)
                {
                    Stall();
                }
            }
        }
    }

    // FixedUpdate is for fixed intervals independent of frame rate
    void FixedUpdate()
    {
        // If we have time to stall, suspend player vertical movement
        if (stallTimer > 0)
        {
            Vector2 velocity = rb.linearVelocity;
            velocity.y = 0f;
            rb.linearVelocity = velocity;
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

    void Stall()
    {
        // Set timer
        stallTimer = stallTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("SwipeUporRecover", false);
            animator.SetBool("SwipeDown", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chunk"))
        {
            FindObjectOfType<ProGen>().SpawnChunk();
        }

        if (other.CompareTag("Obstacle"))
        {
            LoseLife();
            animator.SetBool("Damaged", true);
        }

        if (other.CompareTag("KillBox") && !isDead)
        {
            Die();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ChunkParent"))
        {
            Destroy(other.gameObject, 5f);
        }

        if (other.CompareTag("Obstacle"))
        {
            animator.SetBool("Damaged", false);
        }
    }

    void LoseLife()
    {
        currentLives--;

        if (currentLives <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        FindObjectOfType<GameManager>().isGameActive = false;
        isDead = true;
        gameObject.SetActive(false);
        gameManager.GameOver();
    }
}
