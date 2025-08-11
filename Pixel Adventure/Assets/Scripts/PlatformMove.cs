using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private Transform pointA; // First point of the platform's movement
    [SerializeField] private Transform pointB;
    private Transform MoveTo;
    [SerializeField] private float moveSpeed;
    private PlayerMove playerMove;
    // Start is called before the first frame update
    void Start()
    {
        MoveTo = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, pointA.position) < 0.1f)
        {
            MoveTo = pointB;
        }
        if (Vector2.Distance(transform.position, pointB.position) < 0.1f)
        {
            MoveTo = pointA;
        }
        transform.position = Vector2.MoveTowards(transform.position, MoveTo.position, moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // Make the player a child of the platform when they enter the trigger
            collision.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // Remove the player from the platform's parent when they exit the trigger
            if (gameObject.activeInHierarchy)
            {
                //Debug.Log("Setting parent to NULL");
                collision.transform.parent = null;
            }
        }
    }
}
