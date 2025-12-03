using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputHandler : Singleton<InputHandler>
{
    public Vector2 MouseInput;
    public Vector2 MoveInput;

    public bool isGamepad;

    public event Action onDashReleased;
    public event Action onDashPressed;

    public void OnMouse(InputAction.CallbackContext context)
    {
        MouseInput = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
            onDashPressed?.Invoke();
        if (context.canceled)
            onDashReleased?.Invoke();
    }
}
