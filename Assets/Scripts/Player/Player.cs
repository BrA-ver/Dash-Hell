using System;
using UnityEngine;

public class Player : Character
{
    PlayerCamera camera;
    Pusher pusher;

    [SerializeField] GameObject arrow;

    [Header("Knockback")]
    [SerializeField] float knockbackForce = 25f;

    bool aiming;

    public float KnockbackForce => knockbackForce;

    
   
    protected override void Start()
    {
        base.Start();
        camera = FindFirstObjectByType<PlayerCamera>();
        camera.SetPlayer(this);

        InputHandler.Instance.onDashPressed += OnAim;
        InputHandler.Instance.onDashReleased += OnDash;
        arrow.SetActive(false);

        pusher = GetComponentInChildren<Pusher>();
        pusher.SetPlayer(this);
        pusher.gameObject.SetActive(false);

        health.OnhealthChange.AddListener(OnHealthChange);
        health.OnTookDamage.AddListener(OnTookDamage);
        health.OnDied.AddListener(OnDied);
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
            pusher.gameObject.SetActive(true);
        }
        else
        {

            Vector2 moveInput = InputHandler.Instance.MoveInput;
            Vector3 moveDirection = camera.transform.forward * moveInput.y;
            moveDirection += camera.transform.right * moveInput.x;
            moveDirection.y = 0f;
            moveDirection.Normalize();

            movement.Move(moveDirection);

            pusher.gameObject.SetActive(false);

            if (!aiming)
                camera.ResetOffset();
        }

        if (aiming)
        {
            camera.HandleAimOffset();
        }
        
    }

    #region Input Events
    private void OnAim()
    {
        arrow.SetActive(true);
        aiming = true;
    }

    private void OnDash()
    {
        //Debug.Log("Dash pressed");
        movement.Dash(transform.forward);
        arrow.SetActive(false);
        aiming = false;
        
    }
    #endregion

    #region Health Events
    private void OnHealthChange(int currentHealth)
    {
        GameEvents.Instance.PlayerHealthChanged(currentHealth);
    }

    private void OnTookDamage()
    {
        GameEvents.Instance.PlayerTookDamage();
    }

    private void OnDied()
    {
        GameEvents.Instance.PlayerDied();
        Destroy(gameObject);
        
    }
    #endregion
}
