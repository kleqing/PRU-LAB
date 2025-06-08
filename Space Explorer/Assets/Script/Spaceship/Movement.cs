using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Spaceship Movement")]
    [SerializeField] private float speed;
    
    private Rigidbody2D rb;
    private Vector2 movementInput;
    
    private PlayerInputSystem playerInputActions;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputSystem();
    }

    private void OnEnable()
    {
        playerInputActions.Gameplay.Enable();
        playerInputActions.Gameplay.Move.performed += OnMove;
        playerInputActions.Gameplay.Move.canceled += OnMove;
    }
    
    private void OnDisable()
    {
        playerInputActions.Gameplay.Move.performed -= OnMove;
        playerInputActions.Gameplay.Move.canceled -= OnMove;
        playerInputActions.Gameplay.Disable();
    }
    
    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            movementInput = Vector2.zero;
        }
    }
    
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementInput.x * speed, movementInput.y * speed);
    }
}
