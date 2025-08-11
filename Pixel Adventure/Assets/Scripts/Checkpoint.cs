using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Set the player's checkpoint to this position
            PlayerDead playerDead = collision.GetComponent<PlayerDead>();
            if (playerDead != null)
            {
                anim.SetTrigger("Check");
                playerDead.SetCheckpoint(transform.position);
                //Debug.Log("Checkpoint set at: " + transform.position);
            }
        }
    }
}
