using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    private Rigidbody2D rb;
    private BoxCollider2D box;
    private CircleCollider2D circle;
    
    public float runSpeed = 40f;
    public float gravityScale = 3.0f;
    public float fallingGravityScale = 2.0f;
    public float wallSlideSpeed = 10f;

    public int framesMoveLocked = 15;

    private float horizontalMove = 0f;
    private bool jump = false;
    
    //[SerializeField]
    private bool keepJumping = false;

    [SerializeField]
    private bool onGround = false;

    private bool isWallSliding = false;
    private bool isRightWall = false;

    private int jumpOffWall = 0;
    private int moveLocked = 0;

    bool doWallJump = false;

    private int jumpFrames = 0;

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

        //if (!isWallSliding || jumpOffWall >= 0)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }

        if (moveLocked >= 0)
        {
            float sign = isRightWall ? -1 : 1;
            horizontalMove = Mathf.Abs(horizontalMove) * sign;   
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            keepJumping = true;

            if (onGround)
                jumpFrames = 3;

            onGround = false;

            if (isWallSliding && jumpFrames < 0)
            {
                float sign = isRightWall ? -1 : 1;
                horizontalMove = Mathf.Abs(horizontalMove) * sign;
                //rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
                jumpOffWall = 10;
                moveLocked = framesMoveLocked;
                //controller.m_Grounded = true;
                doWallJump = true;

                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }

        bool triedJumping = false;

        if (keepJumping && Input.GetButton("Jump"))
        {
            rb.gravityScale = gravityScale;
            triedJumping = true;
        }

        if (!triedJumping)
        {
            keepJumping = false;
            rb.gravityScale = gravityScale * fallingGravityScale;
        }

        if (isWallSliding)
            rb.gravityScale = tempGrav;

        
        //Animator Update
        animator.SetFloat("Speed", Math.Abs(horizontalMove));
        animator.SetBool("Jumping", jump);
        animator.SetBool("WallClinging", isWallSliding);

        if (horizontalMove < 0.0)
            animator.SetBool("facingLeft", true);
        else
            animator.SetBool("facingLeft", false);

        if (rb.velocity.y < -1.0)
            animator.SetBool("Falling", true);
        else
            animator.SetBool("Falling", false);

    }

    void FixedUpdate()
    {
        if (doWallJump)
            controller.m_Grounded = true;
        doWallJump = false;

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        if (!onGround && !isWallSliding)
        {
            jumpOffWall--;
        }

        moveLocked--;

        jump = false;
        isWallSliding = false;

        jumpFrames--;
    }

    public void OnLand()
    {
        if (jumpFrames < 0)
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
        Vector2 avgContact = new Vector2(0, 0);
        for (int i = 0; i < collision.contactCount; i++)
        {
            avgContact += collision.GetContact(i).point;
        }
        avgContact /= collision.contactCount;

        if (jumpFrames < 0 &&
            collision.gameObject.layer == LayerMask.NameToLayer("Wall") &&
            Math.Abs(Input.GetAxis("Horizontal")) > 0.3f)
        {
            float dPos = avgContact.x - transform.position.x;
            isRightWall = dPos > 0;

            if (Math.Abs(Mathf.Sign(dPos) - Input.GetAxis("Horizontal")) < 0.1f)
            {
                jumpOffWall = framesMoveLocked;
                isWallSliding = true;

                if (rb.velocity.y < -wallSlideSpeed)
                {
                    rb.gravityScale = 0;
                    rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            isWallSliding = false;
    }
}
