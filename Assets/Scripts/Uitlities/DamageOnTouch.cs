using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Transform enemy;

    private void OnTriggerStay2D(Collider2D collision)
    {
        ADamage damageable = collision.GetComponentInChildren<ADamage>();
        if (damageable == null)
            return;

        damageable.TakeDamage(damage, enemy.position);
    }
}
