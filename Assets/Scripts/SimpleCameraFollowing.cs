using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollowing : MonoBehaviour
{
    
    public Transform target;  // The target object to follow, typically the player
    public Vector3 offset;    // Offset to maintain relative to the target
    public float smoothSpeed = 0.125f; // Smoothing speed for camera movement

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}

