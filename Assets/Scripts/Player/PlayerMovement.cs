using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AMovement
{
    [Header("Configuration")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityStrength;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Rigidbody2D rigidBody;

    private float m_gravityPull;
    private bool m_isGrounded;

    public override void Move(Vector2 moveDirection)
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundCheck.position, groundCheckRadius, Vector2.zero, 0.0f, groundLayer);
        m_isGrounded = hit;
        if (hit && Vector2.Dot(hit.normal, Vector2.up) <= 0.3f)
            m_isGrounded = false;

        rigidBody.AddForce((Vector2.right * moveDirection.x * moveSpeed + -Vector2.up * m_gravityPull) * Time.deltaTime);
        if (m_isGrounded)
            rigidBody.AddForce(Vector2.up * moveDirection.y * jumpHeight * (hit ? 1 : 0), ForceMode2D.Force);

        m_gravityPull = hit ? 0 : m_gravityPull + Time.deltaTime * gravityStrength;
    }
}
