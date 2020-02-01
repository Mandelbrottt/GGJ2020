using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    private Rigidbody2D rb;
    private BoxCollider2D box;
    private CircleCollider2D circle;
    
    public float runSpeed = 40f;
    public float gravityScale = 3.0f;
    public float wallSlideSpeed = 10f;

    public int framesMoveLocked = 15;

    private float horizontalMove = 0f;
    private bool jump = false;
    
    //[SerializeField]
    private bool keepJumping = false;

    [SerializeField]
    private bool onGround = false;

    private bool isWallSliding = true;
    private bool isRightWall = false;

    private int jumpOffWall = 0;
    private int moveLocked = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float tempGrav = rb.gravityScale;

        if (!isWallSliding || jumpOffWall >= 0)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }

        if (moveLocked >= 0)
        {
            horizontalMove = isRightWall 
                                 ? Mathf.Min(0f, horizontalMove) 
                                 : Mathf.Max(0f, horizontalMove);
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            keepJumping = true;
            onGround = false;

            if (isWallSliding)
            {
                float sign = isRightWall ? -1 : 1;
                horizontalMove = horizontalMove * sign * Mathf.Sign(horizontalMove) / 2f;
                rb.velocity = new Vector2(horizontalMove, rb.velocity.y + 15f);
                jumpOffWall = 10;
                moveLocked = framesMoveLocked;
            }
        }

        bool triedJumping = false;
        
        if (rb.velocity.y >= 0)
        {
            box.sharedMaterial.friction = 0;

            if (keepJumping && Input.GetButton("Jump"))
            {
                rb.gravityScale = gravityScale;
                triedJumping = true;
            }
        }

        if (!triedJumping)
        {
            keepJumping = false;
            rb.gravityScale = gravityScale * 2;
        }

        if (isWallSliding)
            rb.gravityScale = tempGrav;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        isWallSliding = false;

        if (!onGround && !isWallSliding)
        {
            jumpOffWall--;
        }

        moveLocked--;
    }

    public void OnLand()
    {
        if (!keepJumping)
        {
            box.sharedMaterial.friction = 0;
            onGround = true;
        }
    }

    // There are two types of terrain colliders, wall and ground.
    // The player will collide with and walk on both, but can only wall slide and jump off of
    // wall terrain
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") &&
            rb.velocity.y < -wallSlideSpeed &&
            Math.Abs(Input.GetAxis("Horizontal")) > 0.3f)
        {
            float dPos = collision.transform.position.x - transform.position.x;
            isRightWall = dPos > 0;

            if (Math.Abs(Mathf.Sign(dPos) - Mathf.Sign(horizontalMove)) < 0.1f)
            {
                jumpOffWall = framesMoveLocked;
                rb.gravityScale = 0;
                isWallSliding = true;
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }
}
