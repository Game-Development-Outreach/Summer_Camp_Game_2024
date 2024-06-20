using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private UnityEvent onCheckpointReached;
    [SerializeField] private string playerTag;

    private bool m_activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_activated)
            return;
        if (collision.tag != playerTag)
            return;

        onCheckpointReached.Invoke();
        m_activated = true;
    }
}
