﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    public Animator anim;

    float hAxis;

    public float movementSpeed;

    public float jumpForce;
    public float jumpTime;
    float currentJumpTime;
    bool isJumping = false;//

    public Transform groundCheckPosition; 
    bool grounded;
    bool wasGrounded;
    public float groundCheckRadius;
    public LayerMask m_Ground;

    [HideInInspector]
    public bool isRight = true;

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
        anim.SetBool("Grounded", grounded);
        //wasGrounded = grounded;
    }

    void Move() {
        Vector2 targetVelocity = new Vector2();
        targetVelocity.x = hAxis * movementSpeed * Time.fixedDeltaTime;
        targetVelocity.y = rb2d.velocity.y;
        rb2d.velocity = targetVelocity;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((isRight && mousePosition.x < transform.position.x) || (!isRight && mousePosition.x > transform.position.x)) Flip();
        if (targetVelocity.x == 0) anim.SetBool("isMoving", false);
        else anim.SetBool("isMoving", true);
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

    void Flip() {
        Vector3 targetScale = transform.localScale;
        targetScale.x *= -1;
        transform.localScale = targetScale;
        isRight = !isRight;
    }

    private void OnDrawGizmosSelected()
    {
        if (gizmos) {
            Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
        }
    }
}
