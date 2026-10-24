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
    private float stallTimer = 0f;
    private bool isGrounded = true;

    public int maxLives = 3;
    public int currentLives;
    public float damageCooldown = 1.5f;
    private float damageTimer = 0f;
    private bool isDead;

    public GameManager gameManager;

    [SerializeField] private Animator animator;

    private Vector2 touchStartPosition;
    public float swipeThreshold = 50f;

    //Sound
    public AudioSource audioSource;
    //when damaged
    public AudioClip PC_Damaged;
    //when killed
    public AudioClip PC_Death;
    //When flaps / jumps
    public AudioClip PC_Flap;
    //when dive down
    public AudioClip PC_Dive;
    // whem stalls / spins
    public AudioClip PC_Stall;


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
            //animator.SetBool("SwipeUporRecover", true);
            isGrounded = false;
        }

        // Requiring the player to NOT be on the ground in order to dive, hopefully to prevent them from forcing themselves through the floor lol
        if (Input.GetKeyDown(KeyCode.S) && !isGrounded)
        {
            Dive();
            //animator.SetBool("SwipeDown", true);
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Stall();
            //animator.SetBool("Spin", true);
        }

        // Timer for stall time
        if (stallTimer > 0)
        {
            stallTimer -= Time.deltaTime;
        }

        // Timer for damage cooldown
        if (damageTimer >0)
        {
            damageTimer -= Time.deltaTime;
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
        //Play player flap sound
        audioSource.PlayOneShot(PC_Flap);
    }

    void Dive()
    {
        // Negate player velocity so they go down
        Vector2 velocity = rb.linearVelocity;
        velocity.y = -diveSpeed;
        rb.linearVelocity = velocity;
        //Play player dive sound
        audioSource.PlayOneShot(PC_Dive);
    }

    void Stall()
    {
        // Set timer
        stallTimer = stallTime;
        //Play player stall sound
        audioSource.PlayOneShot(PC_Stall);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            //animator.SetBool("SwipeUporRecover", false);
            //animator.SetBool("SwipeDown", false);
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
            if (damageTimer <= 0)
            {
                Debug.Log("Player Took Damage");

                LoseLife();
                damageTimer = damageCooldown;
                //animator.SetBool("Damaged", true);

                Debug.Log("Lives Remaining " + currentLives);
                Debug.Log("Damage Cooldown Active for " + damageCooldown + " seconds");
            }
            else
            {
                Debug.Log("Damage Ignored");
            }
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
            //animator.SetBool("Damaged", false);
            //Play player damaged sound
            audioSource.PlayOneShot(PC_Damaged);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
       if (stallTimer > 0 && other.CompareTag("Breakable"))
        {
            Destroy(other.gameObject);
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
        isDead = true;
        gameObject.SetActive(false);
        gameManager.GameOver();
        //Play Death Sound
        audioSource.PlayOneShot(PC_Death);
    }
}

