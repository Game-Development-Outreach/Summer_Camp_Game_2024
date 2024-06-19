using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private UnityEvent onCheckpointReached;

    private bool m_activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_activated)
            return;

        onCheckpointReached.Invoke();
        m_activated = true;
    }
}
