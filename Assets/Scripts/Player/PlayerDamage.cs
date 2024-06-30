using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDamage : ADamage
{
    [Header("Configuration")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float knockbackForce;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform player;
    [Tooltip("The time in seconds after being hit the player is invincible for")]
    [SerializeField] private float iFrames;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> onHit;
    public static UnityEvent onDeath;

    private float m_lastHitTime;

    private void Awake()
    {
        m_lastHitTime = Time.time;
        m_currentHealth = maxHealth;
        onDeath = new UnityEvent();
    }

    public override void TakeDamage(float damage, Vector3 damageSource)
    {
        if (Time.time - m_lastHitTime < iFrames)
            return;

        m_lastHitTime = Time.time;
        m_currentHealth -= damage;
        rigidBody.AddForce(((player.position - damageSource) + Vector3.up).normalized * knockbackForce, ForceMode2D.Impulse);
        onHit.Invoke(m_currentHealth);

        if (m_currentHealth <= 0)
            Die();
        m_currentHealth = maxHealth;
    }

    private void Die()
    {
        m_lastHitTime = Time.time - iFrames;
        onDeath.Invoke();
    }
}
