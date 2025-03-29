using System.Collections;
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

    public GameObject PlayerCanvas;
    public GameObject DethCanvas;

    
    private bool isDead = false;

    public Sprite deathSprite; // Спрайт, который будет показан при смерти
    private SpriteRenderer spriteRenderer;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.freezeRotation = true;

        if (leftButton) AddButtonEvents(leftButton, () => isMovingLeft = true, () => isMovingLeft = false);
        if (rightButton) AddButtonEvents(rightButton, () => isMovingRight = true, () => isMovingRight = false);
        if (jumpButton) jumpButton.onClick.AddListener(TryJump);

        groundCollider = isGroundedPoint.GetComponent<Collider2D>();
        DethCanvas.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isDead) return; // не выполняем управление, если мёртв
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


    private void Die()
    {
        if (isDead) return;

        isDead = true;
        PlayerCanvas.SetActive(false);
        DethCanvas.SetActive(true);

        animator.enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // начнём с выключенной физики

        StartCoroutine(DeathAnimationSequence());
    }


    private IEnumerator DeathAnimationSequence()
    {
        // === 1. Взлет с вращением ===
        float airTime = 0.6f;
        float elapsed = 0f;

        Vector2 jumpForce = new Vector2(0f, 5f); // вверх
        Vector3 rotationPerSecond = new Vector3(0, 0, 720f); // быстрое вращение

        while (elapsed < airTime)
        {
            transform.position += (Vector3)(jumpForce * Time.deltaTime);
            transform.Rotate(rotationPerSecond * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // === 2. Возврат в изначальное положение ===
        transform.rotation = Quaternion.Euler(0f, facingRight ? 0f : 180f, 0f);

        yield return new WaitForSeconds(0.1f); // небольшая пауза перед падением

        // === 3. Падение вниз ===
        rb.isKinematic = false;
        rb.gravityScale = 3f;
        rb.freezeRotation = false;

        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.down * 7f, ForceMode2D.Impulse);

        if (deathSprite != null)
        {
            spriteRenderer.sprite = deathSprite;
        }

        StartCoroutine(FinalizeDeath());
    }


    private IEnumerator FinalizeDeath()
    {
        yield return new WaitForSeconds(1.0f);

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.freezeRotation = true;
        rb.isKinematic = true;
        rb.simulated = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Die();
        }
    }

}