using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAnim : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum AnimState { idle, run, jump, fall }
    // Define animation states (idle 0, run 1, jump 2, fall 3)
    private AnimState currentState;
    private PlayerMove playerMove;
    private PlayerJump playerJump;
    private PlayerWallCheck playerWallCheck;
    private bool isJump, isFall;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerJump = GetComponent<PlayerJump>();
        playerWallCheck = GameObject.FindGameObjectWithTag("WallCheck").GetComponent<PlayerWallCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        // Xmove
        moveAnim();
        // Jump and Fall
        jumpfallAnim();
    }

    private void jumpfallAnim()
    {
        isJump = rb.velocity.y > 0.3f;
        anim.SetBool("isJump", isJump);
        isFall = rb.velocity.y < -0.3f;
        if (isJump && !playerJump.isGround)
        {
            currentState = AnimState.jump; // 2
        }
        else if (isFall && !playerJump.isGround)
        {
            currentState = AnimState.fall; // 3
        }
        anim.SetInteger("states", (int)currentState);
    }

    private void moveAnim()
    {
        if (playerMove.MoveController != 0)
        {
            currentState = AnimState.run; // 1
        }
        else
        {
            currentState = AnimState.idle; // 0
        }
    }
}
