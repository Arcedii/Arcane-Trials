using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;

    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float groundCheckDistance = 0.1f; // Уменьшите groundCheckDistance

    public Transform isGroundedPoint;
    public LayerMask groundLayer;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private bool isJumping = false;
    private bool isGrounded = false;
    private bool canJump = true;
    private bool facingRight = true;

    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D groundCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.freezeRotation = true;

        if (leftButton) AddButtonEvents(leftButton, () => isMovingLeft = true, () => isMovingLeft = false);
        if (rightButton) AddButtonEvents(rightButton, () => isMovingRight = true, () => isMovingRight = false);
        if (jumpButton) jumpButton.onClick.AddListener(TryJump);

        groundCollider = isGroundedPoint.GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
        Jump();
    }

    private void Move()
    {
        float moveInput = 0;
        if (isMovingLeft) moveInput = -1;
        if (isMovingRight) moveInput = 1;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0 && !facingRight) Flip();
        if (moveInput < 0 && facingRight) Flip();

        if (moveInput != 0)
        {
            animator.SetInteger("playerState", 1);
        }
        else if (isGrounded)
        {
            animator.SetInteger("playerState", 0);
        }
    }

    private void Jump()
    {
        if (isJumping && canJump && isGrounded)
        {           
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetInteger("playerState", 2);
            isGrounded = false;
            canJump = false;
            jumpButton.interactable = false;
        }
        isJumping = false;
    }

    private void TryJump()
    {
        if (canJump && isGrounded)
        {
            isJumping = true;
        }
    }

    private void CheckGround()
    {
        bool wasGrounded = isGrounded;
        RaycastHit2D hit = Physics2D.Raycast(isGroundedPoint.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        if (isGrounded && !wasGrounded)
        { 
            if (!canJump)
            {
                EnableJump();
            }
        } 
    }

    private void EnableJump()
    {
        canJump = true;
        jumpButton.interactable = true;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void AddButtonEvents(Button button, System.Action onDown, System.Action onUp)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null) trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDown.callback.AddListener((data) => { onDown(); });
        trigger.triggers.Add(pointerDown);

        EventTrigger.Entry pointerUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUp.callback.AddListener((data) => { onUp(); });
        trigger.triggers.Add(pointerUp);
    }
}