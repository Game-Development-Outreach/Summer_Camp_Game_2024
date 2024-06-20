using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooting : AAttack
{
    public UnityEvent<GameObject> onHit;
    [SerializeField] private UnityEvent onShoot;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float spawnDistance;
    [SerializeField] private Transform player;

    private float m_lastShootTime;

    public override void Attack(Vector3 mousePosition)
    {
        if ((Time.time - m_lastShootTime) < timeBetweenShots && m_lastShootTime != 0)
            return;
        Vector3 toMouse = (mousePosition - player.position).normalized;

        Projectile instantiatedProjectile = Instantiate(projectilePrefab);
        instantiatedProjectile.transform.position = player.position + toMouse * spawnDistance;
        instantiatedProjectile.Init(toMouse, this);

        m_lastShootTime = Time.time;
    }
}
