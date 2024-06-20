using UnityEngine;

public enum PlayerState
{
    Idle,
    Moving,
    Shooting
}

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeedMultiplier = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private PlayerState currentState;
    private Animator animator;
    private bool facingRight = true;
    private bool isGrounded = false;
    private Rigidbody2D rb;

    void Start()
    {
        currentState = PlayerState.Idle;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                HandleIdleState();
                break;

            case PlayerState.Moving:
                HandleMovingState();
                break;

            case PlayerState.Shooting:
                HandleShootingState();
                break;
        }
    }

    void HandleIdleState()
    {
        animator.SetBool("isMoving", false);
        animator.SetBool("isShooting", false);

        if (Input.GetAxis("Horizontal") != 0)
        {
            currentState = PlayerState.Moving;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentState = PlayerState.Shooting;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void HandleMovingState()
    {
        animator.SetBool("isMoving", true);
        animator.SetBool("isShooting", false);

        // Movimiento del jugador
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);

        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(movement * speed * runSpeedMultiplier * Time.deltaTime);
        }
        else
        {
            transform.Translate(movement * speed * Time.deltaTime);
        }

        // Voltear el personaje
        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }

        if (moveHorizontal == 0)
        {
            currentState = PlayerState.Idle;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentState = PlayerState.Shooting;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void HandleShootingState()
    {
        animator.SetBool("isMoving", false);
        animator.SetBool("isShooting", true);
        ShootProjectile();
        currentState = PlayerState.Idle;
    }

    void ShootProjectile()
    {
        Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // Detección de colisiones y comparación de vectores
        Vector2 contactPoint = collision.contacts[0].point;
        Vector2 center = collision.collider.bounds.center;
        Vector2 direction = contactPoint - center;

        if (direction.x > 0)
        {
            Debug.Log("Colisión desde la derecha");
        }
        else
        {
            Debug.Log("Colisión desde la izquierda");
        }
    }

    void OnDrawGizmos()
    {
        if (projectileSpawnPoint != null)
        {
            // Dibujar una línea desde el punto de disparo en la dirección del disparo
            Gizmos.color = Color.red;
            Gizmos.DrawLine(projectileSpawnPoint.position, projectileSpawnPoint.position + projectileSpawnPoint.up * 2);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}