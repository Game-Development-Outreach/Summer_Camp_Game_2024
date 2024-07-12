using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField] new private Rigidbody2D rigidbody;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float reflectBulletSpeed;
    [SerializeField] private float despawnTime;
    [SerializeField] private string reflectiveTag;
    [SerializeField] private string breakTag;
    [SerializeField] public string dooropenTag;

    private PlayerShooting m_attack;
    private Vector3 m_travelDirection;
    public static event Action<GameObject> OnCollisionDetected;

    public void Init(Vector3 shootDirection, PlayerShooting attack)
    {
        m_attack = attack;
        rigidbody.AddForce(shootDirection * bulletSpeed, ForceMode2D.Impulse);
        m_travelDirection = shootDirection;
        Destroy(gameObject, despawnTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_attack.onHit.Invoke(collision.gameObject);
        IInteractable interactable = collision.gameObject.GetComponentInChildren<IInteractable>();
        if (interactable != null)
        {
            interactable.Interact();
        }

        if (collision.gameObject.tag == reflectiveTag)
        {
            Vector3 reflect = Vector3.Reflect(m_travelDirection, collision.GetContact(0).normal).normalized;
            rigidbody.velocity = reflect * reflectBulletSpeed;
            m_travelDirection = reflect.normalized;
            return;
        }
        if (collision.gameObject.tag == breakTag)
        {
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == dooropenTag)
        {
            OnCollisionDetected?.Invoke(collision.collider.gameObject);
        }

        Destroy(gameObject);
    }
}
