using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadelineEnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveDelay;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rigidBody;

    [SerializeField] private Transform enemy;
    [HideInInspector] public Vector3 startingPosition;

    void Start()
    {
        PlayerMovement.onMove.AddListener((Vector3 moveDirection) => { StartCoroutine(Move(moveDirection)); });
        PlayerDamage.onDeath.AddListener(() => { enemy.position = startingPosition; });
    }

    private IEnumerator Move(Vector3 moveDirection)
    {
        yield return new WaitForSeconds(moveDelay);

        rigidBody.AddForce(moveDirection * moveSpeed);
    }
}
