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

    private float horizontalMove = 0f;
    private bool jump = false;
    
    //[SerializeField]
    private bool keepJumping = false;

    [SerializeField]
    private bool onGround = false;

    private bool overrideGravity = true;

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
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            keepJumping = true;
            onGround = false;
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

        if (overrideGravity)
            rb.gravityScale = tempGrav;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        overrideGravity = false;
    }

    public void OnLand()
    {
        if (!keepJumping)
        {
            box.sharedMaterial.friction = 0;
            onGround = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") &&
            rb.velocity.y < -wallSlideSpeed)
        {
            rb.gravityScale = 0;
            overrideGravity = true;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }
}
