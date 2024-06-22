using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AMovement
{
    [Header("Configuration")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityStrength;
    [SerializeField] private float wallSlideSpeed;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private List<Transform> wallChecks;
    [SerializeField] private float wallCheckRadius;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Rigidbody2D rigidBody;

    private float m_gravityPull;
    private GameObject m_wallLastFrame;
    private bool m_allowedToWallJump;
    private Vector2 m_wallNormal;

    public override void Move(Vector2 moveDirection)
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundCheck.position, groundCheckRadius, Vector2.zero, 0.0f, groundLayer);
        bool isGrounded = hit && Vector2.Dot(hit.normal, Vector2.up) > 0.3f;

        bool isOnWall = false;
        GameObject currentWall = null;
        for (int i = 0; i < wallChecks.Count; i++)
        {
            RaycastHit2D wallHit = Physics2D.CircleCast(wallChecks[i].position, groundCheckRadius, Vector2.zero, 0.0f, groundLayer);
            if (wallHit)
            {
                isOnWall = true;
                currentWall = wallHit.transform.gameObject;
                m_wallNormal = wallHit.normal;
                break;
            }
        }

        m_allowedToWallJump = !((m_wallLastFrame == currentWall) && (currentWall != null && m_wallLastFrame != null));

        float gravityModifier = m_gravityPull;
        if (isOnWall && m_allowedToWallJump)
            gravityModifier = -wallSlideSpeed;

        rigidBody.AddForce((Vector2.right * moveDirection.x * moveSpeed + -Vector2.up * gravityModifier) * Time.deltaTime);
        m_gravityPull = ((isGrounded || isOnWall) && m_allowedToWallJump) ? 0 : m_gravityPull + Time.deltaTime * gravityStrength;

        if (moveDirection.y == 0)
            return;

        if (!m_allowedToWallJump)
            return;
        if (isGrounded)
            rigidBody.AddForce(Vector2.up * moveDirection.y * jumpHeight, ForceMode2D.Force);
        else if(isOnWall)
            rigidBody.AddForce((Vector2.up + m_wallNormal).normalized * moveDirection.y * jumpHeight, ForceMode2D.Force);

        m_wallLastFrame = currentWall;

    }
}
