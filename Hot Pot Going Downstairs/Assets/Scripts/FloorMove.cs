using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        if (transform.position.y > 5f) // Assuming the top of the screen is at y = 5
        {
            Destroy(gameObject); // Destroy the floor when it moves out of view
            transform.parent.GetComponent<FloorManager>().SpawnFloor(); // Spawn a new floor
        }
    }
}
