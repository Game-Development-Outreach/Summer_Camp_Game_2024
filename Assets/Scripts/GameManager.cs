using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private GameObject player;

    [Header("Events")]
    [SerializeField] private UnityEvent onCheckpointReachedGM;
    [SerializeField] private UnityEvent onRespawned;
    [SerializeField] private UnityEvent onGameStart;

    private Transform m_spawnPoint;

    public void Start() => onGameStart.Invoke();

    public void Respawn()
    {
        onRespawned.Invoke();
        player.transform.position = m_spawnPoint.transform.position;
    }

    public void SetSpawnPoint(Transform checkpoint)
    {
        onCheckpointReachedGM.Invoke();
        m_spawnPoint = checkpoint;
    }
}
