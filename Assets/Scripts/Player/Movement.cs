using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float airResistance;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityStrength;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Rigidbody2D rigidBody;

    private InputActions _inputActions;
    private Vector2 _moveDirection;
    private float _gravityPull;

    private bool _isGrounded;

    void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Enable();

        _inputActions.Default.Movement.performed += ctx =>
        {
            Vector2 inputDirection = ctx.ReadValue<Vector2>();
            _moveDirection = new Vector2(inputDirection.x, inputDirection.y > 0 ? 1 : 0);
        };
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundCheck.position, groundCheckRadius, Vector2.zero, 0.0f, groundLayer);
        _isGrounded = hit;
        if (hit && Vector2.Dot(hit.normal, Vector2.up) <= 0.3f)
            _isGrounded = false;

        rigidBody.AddForce((Vector2.right * _moveDirection.x * moveSpeed + -Vector2.up * _gravityPull) * Time.deltaTime);
        rigidBody.AddForce(Vector2.up * _moveDirection.y * jumpHeight * (hit ? 1 : 0), ForceMode2D.Force);
        rigidBody.AddForce(-rigidBody.velocity * airResistance);

        _gravityPull = hit ? 0 : _gravityPull + Time.deltaTime * gravityStrength;

        _moveDirection = new Vector2(_moveDirection.x, 0);
    }

    private void OnDisable() => _inputActions.Disable();
}
