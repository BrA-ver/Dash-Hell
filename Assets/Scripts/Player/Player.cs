using System;
using UnityEngine;

public class Player : Character
{
    PlayerCamera camera;
    Pusher pusher;
    PlayerHealth playerHealth;

    [SerializeField] GameObject arrow;

    [Header("Knockback")]
    [SerializeField] float knockbackForce = 25f;

    [Header("Dash")]
    [SerializeField, Range(0f, 5f)] float dashInvincibility = 2f;

    [Header("Fall Delay")]
    [SerializeField] float fallDelay = 2f;
    float fallCounter;

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

        health = health as PlayerHealth;

        health.OnhealthChange.AddListener(OnHealthChange);
        health.OnTookDamage.AddListener(OnTookDamage);
        health.OnDied.AddListener(OnDied);

        playerHealth = health as PlayerHealth;
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

        AudioManager.Instance?.PlaySFX("Aim");
    }

    private void OnDash()
    {
        //Debug.Log("Dash pressed");
        movement.Dash(transform.forward);
        arrow.SetActive(false);
        aiming = false;
        playerHealth.MakeInvincible(dashInvincibility);

        AudioManager.Instance?.PlaySFX("Dash");
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

        AudioManager.Instance?.PlaySFX("Take Damage");
    }

    private void OnDied()
    {
        GameEvents.Instance.PlayerDied();
        Destroy(gameObject);
        
    }
    #endregion

    protected override void BoostGravityWhenOffGround()
    {
        Debug.Log(groundDetector.OnGround);
        if (!groundDetector.OnGround)
        {
            fallCounter -= Time.deltaTime;
            if (fallCounter <= 0f)
            {
                movement.BoostGravity();
            }
        }
        else
        {
            fallCounter = fallDelay;
        }
    }
}
