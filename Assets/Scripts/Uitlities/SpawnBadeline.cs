using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBadeline : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;

    public void Spawn()
    {
        GameManager.spawnBadeline.Invoke(spawnPosition.position);
    }
}
