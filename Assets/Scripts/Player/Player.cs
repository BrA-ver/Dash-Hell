using System;
using UnityEngine;

public class Player : Character
{
    PlayerCamera camera;

    [SerializeField] GameObject arrow;

    protected override void Start()
    {
        base.Start();
        camera = FindFirstObjectByType<PlayerCamera>();
        camera.SetPlayer(this);

        InputHandler.Instance.onDashPressed += OnAim;
        InputHandler.Instance.onDashReleased += OnDash;
        arrow.SetActive(false);
    }

    private void OnDisable()
    {
        InputHandler.Instance.onDashReleased -= OnDash;
        InputHandler.Instance.onDashPressed -= OnAim;
    }

    protected override void Update()
    {
        base.Update();

        if (movement.Dashing)
        {
            movement.DashCounter();
        }
        else
        {

            Vector2 moveInput = InputHandler.Instance.MoveInput;
            Vector3 moveDirection = camera.transform.forward * moveInput.y;
            moveDirection += camera.transform.right * moveInput.x;
            moveDirection.y = 0f;
            moveDirection.Normalize();

            movement.Move(moveDirection);
        }
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnAim()
    {
        arrow.SetActive(true);
    }

    private void OnDash()
    {
        Debug.Log("Dash pressed");
        movement.Dash(transform.forward);
        arrow.SetActive(false);
    }
}
