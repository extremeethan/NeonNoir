using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    [Header("AFK & Timeout Settings")]
    [SerializeField] private float afkDelay = 0.5f;
    [SerializeField] private float letGoDelay = 0.5f;

    private float afkTimer = 0f;
    private bool isAfkActive = false;

    private const string PARAM_DASH = "IsDash";
    private const string PARAM_DIVE = "IsDive";
    private const string PARAM_SWIPE_UP = "IsSwipeUp";
    private const string PARAM_HURT = "IsHurt";
    private const string PARAM_AFK = "IsAFK";
    private const string PARAM_LET_GO = "IsLetGo";
    private const string PARAM_SUCCESS = "IsSuccess";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandlePlayerInputs();
        HandleAfkAndLetGoTimer();
    }

    private void HandlePlayerInputs()
    {
        bool pressD = Input.GetKey(KeyCode.D);
        bool pressS = Input.GetKey(KeyCode.S);
        bool pressW = Input.GetKey(KeyCode.W);

        // Reset AFK timers on input
        if (Input.anyKeyDown || pressD || pressS || pressW)
        {
            ResetAfkTimers();
        }

        // 1. Dash
        animator.SetBool(PARAM_DASH, pressD);

        // 2. Success Logic
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Set IsSuccess true when D is hit
            animator.SetBool(PARAM_SUCCESS, true);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
        {
            // Reset IsSuccess if player chooses a different action
            animator.SetBool(PARAM_SUCCESS, false);
        }

        // 3. Dive & SwipeUp
        animator.SetBool(PARAM_DIVE, pressS);
        animator.SetBool(PARAM_SWIPE_UP, pressW);
    }

    private void HandleAfkAndLetGoTimer()
    {
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            afkTimer += Time.deltaTime;

            if (afkTimer >= afkDelay && !isAfkActive)
            {
                isAfkActive = true;
                animator.SetBool(PARAM_AFK, true);
            }
            else if (afkTimer >= (afkDelay + letGoDelay) && isAfkActive)
            {
                animator.SetBool(PARAM_LET_GO, true);
            }
        }
    }

    private void ResetAfkTimers()
    {
        afkTimer = 0f;
        isAfkActive = false;
        animator.SetBool(PARAM_AFK, false);
        animator.SetBool(PARAM_LET_GO, false);
        // NOTE: We do NOT reset PARAM_SUCCESS here so Spin can complete and transition into SuccessSpin!
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool(PARAM_HURT, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool(PARAM_HURT, false);
    }
}