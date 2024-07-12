using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject objectToDisable;
    public Projectile ProjectileTag;

    void OnEnable()
    {
        Projectile.OnCollisionDetected += HandleCollision;
    }

    void OnDisable()
    {
        Projectile.OnCollisionDetected -= HandleCollision;
    }

    void HandleCollision(GameObject disabler)
    {
        // Check if the disabler object is the one we want
        if (disabler.tag == ProjectileTag.dooropenTag)
        {
            // Disable the target object
            objectToDisable.SetActive(false);
        }
    }
}
