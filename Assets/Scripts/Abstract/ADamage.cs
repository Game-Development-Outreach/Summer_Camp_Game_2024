using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ADamage : MonoBehaviour
{
    protected float m_currentHealth;
    public abstract void TakeDamage(float damage, Vector3 damgeSource);
}
