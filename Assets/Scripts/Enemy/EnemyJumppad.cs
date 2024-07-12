using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Extremely simple code that we apply to a game object as a "jumppad", used mainly for the top of enemies being given functionality to bounce the player upwards. 
public class EnemyJumppad : MonoBehaviour
{
    public float upwardForce = 10f; // Adjust this value to set the upward momentum

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with has a Rigidbody2D
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Apply an upward force to the object
            rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
        }
    }
}
