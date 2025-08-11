using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EatFruits : MonoBehaviour
{
    private int fruitCount; 
    public TextMeshProUGUI fruitCountText; // Reference to the UI text component
    // Start is called before the first frame update
    void Start()
    {
        fruitCount = 0; // Initialize fruit count
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruits"))
        {
            fruitCount++;
            fruitCountText.text = ":" + fruitCount.ToString(); // Update the UI text with the current fruit count
            // destroy the fruit object after collection
            Destroy(collision.gameObject);
        }
    }
}
