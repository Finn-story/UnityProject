using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movespeed = 5;
    public float MoveController;
    public AudioSource runSound;
    private PlayerJump playerJump;
    private SpriteRenderer spriteRenderer;
    [SerializeField] PlayerWallCheck playerWallCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerJump = GetComponent<PlayerJump>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //playerWallCheck = GetComponentInChildren<PlayerWallCheck>();
        //playerWallCheck = GameObject.FindGameObjectWithTag("WallCheck").GetComponent<PlayerWallCheck>();
        playerWallCheck = FindAnyObjectByType<PlayerWallCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        XMove();
    }

    private void XMove()
    {
        // Change GetAxisRaw to GetAxis for smoother movement
        MoveController = Input.GetAxisRaw("Horizontal");
        if(MoveController != 0 && playerJump.isGround)
        {
            if(!runSound.isPlaying)
            {
                // Play running sound when the player starts moving
                runSound.Play();
            }
        }
        else
        {
            // Stop running sound when the player stops moving
            runSound.Stop();
        }
        // play the sound first, then move the player,consider the wall jump
        if (!playerJump.wallJumping && rb.bodyType != RigidbodyType2D.Static)
        {
            rb.velocity = new Vector2(MoveController * movespeed, rb.velocity.y);
            // Handle moving left and right
            if (MoveController < 0 && rb.velocity.x < 0)
                transform.localScale = new Vector2(-1, 1); // can also change the wallcheck direction
                                                           //spriteRenderer.flipX = true; cannot
            else if (MoveController > 0)
                transform.localScale = new Vector2(1, 1); // can also change the wallcheck direction
                                                          //spriteRenderer.flipX = false; cannot
        }
    }
}
