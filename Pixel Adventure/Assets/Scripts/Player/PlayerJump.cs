using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float jumpforce = 7;
    [SerializeField] private float wallJumpSpeed = 20;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float isGroundCheck = 1.1f; // Distance to check for ground
    [SerializeField] private float jumpAddTime = 0.3f; // Time to add jump force
    private float jumpAddController = 0f; // Controller for jump add time
    [SerializeField] private float jumpAddForce = 2f; // Additional force to apply while holding jump
    [SerializeField] private float fallAddForce = 3f; // Additional force to apply while falling
    public bool isGround, wallJumping, banDropSpeed;
    private bool JumpController, isJumping, doubleJump,doubleJumpped;
    public AudioSource jumpSound;
    public AudioSource doubleJumpSound;
    private PlayerWallCheck playerWallCheck;
    private PlayerMove playerMove;
    [SerializeField] float walljumpforce = 16f; // Force applied when performing a wall jump
    [SerializeField] float currentyvelocity; // To store the current y velocity of the player
    [SerializeField] float offset;
    [SerializeField] float dropSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //playerWallCheck = GetComponentInChildren<PlayerWallCheck>();
        playerWallCheck = GameObject.FindGameObjectWithTag("WallCheck").GetComponent<PlayerWallCheck>();
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.bodyType != RigidbodyType2D.Static)
            Jump();
    }

    IEnumerator WallJumping()
    {
        wallJumping = true;
        banDropSpeed = true;
        yield return new WaitForSeconds(0.3f); // wait for a short duration to allow the wall jump not to be interrupted by
                                               // horizontal movement
        wallJumping = false;
        banDropSpeed = false;
    }

    private void WallJump()
    {
        if(playerWallCheck.byTheWall)
        {
            if (!isGround)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    transform.localScale = new Vector2(transform.localScale.x * -1, 1); // Flip the player to face away from the wall
                    rb.velocity = new Vector2((wallJumpSpeed + offset) * transform.localScale.x, walljumpforce);
                    jumpSound.Play();
                    StartCoroutine(WallJumping());
                }
                else
                {
                    if (!banDropSpeed)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, -dropSpeed);
                    }
                }
            }
            // on the ground near the wall
            else
            {
                if(JumpController)
                {
                    transform.localScale = new Vector2(transform.localScale.x * -1, 1); // Flip the player to face away from the wall
                    rb.velocity = new Vector2((wallJumpSpeed + offset) * transform.localScale.x, walljumpforce);
                    jumpSound.Play();
                    StartCoroutine(WallJumping());
                }
            }
        }
    }
    private void Jump()
    {
        JumpController = Input.GetButtonDown("Jump"); // Check if the jump button is pressed
        // Check if the player is grounded by using a raycast
        isGround = Physics2D.Raycast(transform.position, Vector2.down, isGroundCheck, GroundLayer);
        anim.SetBool("isGround", isGround);

        // in the air and not grounded double jump
        if (!isGround && !doubleJump && !doubleJumpped)
        {
            doubleJump = true; // Allow double jump if not grounded and not jumping
        }

        WallJump(); // Check for wall jump

        // jumped and double jumped
        if (JumpController && isGround)
        {
            // Play jump sound when the player jumps
            jumpSound.Play();

            rb.velocity = new Vector2(rb.velocity.x, jumpforce);

            jumpAddController = 0f;

            isJumping = true;

            doubleJump = true; // Reset double jump on ground jump
            doubleJumpped = false;
        }

        // Check for double jump
        if (doubleJump && !isGround && JumpController && !playerWallCheck.byTheWall)
        {
            doubleJumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpAddController = 0f;
            isJumping = true;
            anim.SetTrigger("isDoubleJump");
            doubleJump = false; // Disable double jump after the jump
            doubleJumpped = true; // Mark that double jump has been used
        }
        if(isGround)
        {
            doubleJumpped = false; // Reset double jump used flag when grounded
        }
        if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        if(isJumping)
        {
            if (jumpAddController < jumpAddTime)
            {
                rb.velocity += new Vector2(0, -Physics2D.gravity.y * Time.deltaTime * jumpAddForce);
                jumpAddController += Time.deltaTime;
            }
            else
                isJumping = false;
        }
        else
        {
            rb.velocity -= new Vector2(0, -Physics2D.gravity.y * Time.deltaTime * fallAddForce);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a ray to visualize the ground check
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - isGroundCheck));
    }
}   
