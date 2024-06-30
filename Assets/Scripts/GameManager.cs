using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject badeline;

    [Header("Events")]
    [SerializeField] private UnityEvent onCheckpointReachedGM;
    [SerializeField] private UnityEvent onRespawned;
    [SerializeField] private UnityEvent onGameStart;

    public static UnityEvent<Vector3> spawnBadeline;
    public static UnityEvent despawnBadeline;

    private Transform m_spawnPoint;
    private GameObject m_instantiatedBadeline;

    public void Awake()
    {
        spawnBadeline = new UnityEvent<Vector3>();
        despawnBadeline = new UnityEvent();

        spawnBadeline.AddListener(SpawnBadeline);
        despawnBadeline.AddListener(DespawnBadeline);
    }

    public void Start()
    {
        onGameStart.Invoke();
        //Temporary, will refactor later
        PlayerDamage.onDeath.AddListener(Respawn);
    }

    public void Respawn()
    {
        onRespawned.Invoke();
        player.transform.position = m_spawnPoint.transform.position;
    }

    public void SpawnBadeline(Vector3 spawnPoint)
    {
        if (m_instantiatedBadeline != null)
            Destroy(m_instantiatedBadeline);

        m_instantiatedBadeline = Instantiate(badeline);
        m_instantiatedBadeline.transform.position = spawnPoint;
        m_instantiatedBadeline.GetComponent<BadelineEnemyMovement>().startingPosition = spawnPoint;
    }

    public void DespawnBadeline() => Destroy(m_instantiatedBadeline);

    public void SetSpawnPoint(Transform checkpoint)
    {
        onCheckpointReachedGM.Invoke();
        m_spawnPoint = checkpoint;
    }
}
