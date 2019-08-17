using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;

    float hAxis;

    public float movementSpeed;

    public float jumpForce;
    public float jumpTime;
    float currentJumpTime;
    bool isJumping = false;//

    public Transform groundCheckPosition; 
    bool grounded;
    public float groundCheckRadius;
    public LayerMask m_Ground;

    public bool gizmos;
    

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        HandleJump();
        CheckForCollisions();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void CheckForCollisions() {
        grounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, m_Ground);
    }

    void Move() {
        Vector2 targetVelocity = new Vector2();
        targetVelocity.x = hAxis * movementSpeed * Time.fixedDeltaTime;
        targetVelocity.y = rb2d.velocity.y;

        rb2d.velocity = targetVelocity;
    }

    void HandleJump() {
        if (Input.GetKeyDown(KeyCode.Space)&&grounded)
        {
            isJumping = true;
            currentJumpTime = jumpTime;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
        if (Input.GetKey(KeyCode.Space)&&isJumping)
        {
            currentJumpTime -= Time.deltaTime;
            if (currentJumpTime > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (gizmos) {
            Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
        }
    }
}
