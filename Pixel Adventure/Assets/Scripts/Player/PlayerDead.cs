using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    private Animator anim;
    private Vector2 pos;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            anim.SetTrigger("isDead"); // Trigger the hit animation 
            rb.bodyType = RigidbodyType2D.Static; // Stop the player from moving
        }
    }
    public void ReviveTransport()
    {
        transform.position = pos;
    }
    public void Revive()
    {
        rb.bodyType = RigidbodyType2D.Dynamic; // Allow the player to move again
    }

    public void SetCheckpoint(Vector2 newPos)
    {
        pos = newPos;
    }
}
