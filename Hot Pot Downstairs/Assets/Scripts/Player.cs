using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int Hp;
    [SerializeField] GameObject HpBar;
    [SerializeField] TextMeshProUGUI scoreText;
    private Animator animator;
    private SpriteRenderer Renderer;
    GameObject currentFloor;
    int score = 0;
    float scoreTime = 0;
    AudioSource deathSound;
    [SerializeField] GameObject replayButton;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Hp = 10; // Initialize player health
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
        deathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        if (rb.velocity.x < -0.3f)
        {
            Renderer.flipX = true; // Flip the player sprite when moving left
            animator.SetBool("isRun", true); // Set running animation if moving
        }
        else if (rb.velocity.x > 0.3f)
        {
            Renderer.flipX = false; // Unflip the player sprite when moving right
            animator.SetBool("isRun", true); // Set running animation if moving
        }
        else
        {
            animator.SetBool("isRun", false); // Set idle animation if not moving
        }
        UpdateHpBar(); // Update the health bar UI
        UpdateScore(); // Update the score based on time
        bool flowControl = Die();
        if (!flowControl)
        {
            return;
        }
    }

    private bool Die()
    {
        if (Hp <= 0)
        {
            //deathSound.Play(); // Play death sound
            Time.timeScale = 0f; // Stop the game time
            replayButton.SetActive(true);
            return false; // Stop further updates if player is dead
        }

        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (collision.contacts[0].normal.y > 0.5f)
            {
                currentFloor = collision.gameObject;
                ModifyHp(1); // Increase HP when landing on the floor
                //collision.gameObject.GetComponent<AudioSource>().Play(); // Play landing sound
            }
        }
        else if (collision.gameObject.CompareTag("Nails"))
        {
            if (collision.contacts[0].normal.y > 0.5f)
            {
                currentFloor = collision.gameObject;
                ModifyHp(-3); // Decrease HP when landing on the nails
                animator.SetTrigger("hurt"); // Trigger hurt animation
                //collision.gameObject.GetComponent<AudioSource>().Play(); // Play hurt sound
            }
        }
        else if (collision.gameObject.CompareTag("Ceiling"))
        {
            if(currentFloor != null)
            {
                currentFloor.GetComponent<BoxCollider2D>().enabled = false; // Disable the collider of the current floor
            }
            ModifyHp(-3); // Decrease HP when hitting the ceiling
            animator.SetTrigger("hurt"); // Trigger hurt animation
            //collision.gameObject.GetComponent<AudioSource>().Play(); // Play hurt sound
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("DeathLine"))
        {
            Hp = 0; // Set HP to 0 when hitting the death line
        }
    }
    void ModifyHp(int num)
    {
        Hp += num;
        if (Hp > 10)
        {
            Hp = 10; // Cap HP at a maximum value
            score++;
        }
        else if (Hp <= 0)
        {
            Hp = 0; // Ensure HP does not go below zero
            // Handle player death (e.g., respawn, game over)
        }
    }
    void UpdateHpBar()
    {
        for (int i = 0; i < HpBar.transform.childCount; i++)
        {
            if (i < Hp)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true); // Show the health bar segment
            }
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false); // Hide the health bar segment
            }
        }
    }
    void UpdateScore()
    {
        scoreTime += Time.deltaTime; // Increment score time
        if (scoreTime >= 2f) // Update score every second
        {
            score++;
            scoreText.text = score.ToString(); // Update the score text
            scoreTime = 0f; // Reset score time
        }
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Hot Pot Downstairs"); // reload scene
    }
}
