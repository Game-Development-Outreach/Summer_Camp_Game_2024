using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    public Transform pointA;  // Transform of the first point
    public Transform pointB;  // Transform of the second point
    public float speed = 1.0f;  // Speed of movement

    private Vector3 originalPosition;  // The original position of the object
    private Transform target;  // Current target point
    private bool shouldReset = false;  // Flag to control reset behavior
    private bool isResetting = false;  // Flag to track resetting process
    private PlayerDamage playerDamage;

    void Start()
    {
        // Store the original position of the object
        originalPosition = transform.position;

        // Set the initial target to point A
        target = pointA;

        playerDamage = FindObjectOfType<PlayerDamage>();
        if (playerDamage == null)
        {
            Debug.LogError("PlayerDamage script not found in the scene.");
        }

        // Subscribe to player death event
        playerDamage.onDeath.AddListener(Death);
    }

    void Update()
    {
        if (!isResetting)
        {
            // Move the object towards the target point
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Check if the object has reached the target point
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                // Switch target
                target = (target == pointA) ? pointB : pointA;
            }
        }
        else
        {
            // Reset position to original position
            transform.position = originalPosition;

            // Reset target to point A
            target = pointA;

            // Resetting process complete
            isResetting = false;
        }


    }

    void Death()
    {
        // Trigger reset process
        isResetting = true;
    }

}
