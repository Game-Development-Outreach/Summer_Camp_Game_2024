using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AMovement movement;
    [SerializeField] private AAttack attack;
    [SerializeField] new private Camera camera;

    private InputActions m_inputActions;
    private Vector2 m_moveDirection;
    private Vector3 m_mouseWorldPosition;

    private void Awake()
    {
        m_inputActions = new InputActions();
        m_inputActions.Enable();

        m_inputActions.Default.Movement.performed += ctx =>
        {
            Vector2 inputDirection = ctx.ReadValue<Vector2>();
            m_moveDirection = new Vector2(inputDirection.x, inputDirection.y > 0 ? 1 : 0);
        };

        m_inputActions.Default.MouseMoved.performed += ctx =>
        {
            Vector2 mouseWorldPosition = camera.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            m_mouseWorldPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0.0f);
        };

        m_inputActions.Default.Shoot.performed += ctx =>
        {
            attack.Attack(m_mouseWorldPosition);
        };

        m_inputActions.Default.Jump.performed += ctx =>
        {
            m_moveDirection = new Vector2(m_moveDirection.x, 1);
        };
    }

    private void Update()
    {
        movement.Move(m_moveDirection);
        m_moveDirection = new Vector2(m_moveDirection.x, 0);
    }

    private void OnDisable() => m_inputActions.Disable();
    private void OnEnable() => m_inputActions.Enable();
}
