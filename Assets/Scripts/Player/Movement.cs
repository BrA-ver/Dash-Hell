using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    protected private Rigidbody rb;

    protected Vector3 moveDirection;

    protected Vector3 velocity;
    protected Vector3 yVelocity;

    [Header("Movement")]
    [SerializeField] protected float maxSpeed = 8f;
    [SerializeField] protected float moveForce = 100f;

    [Header("Dash")]
    [SerializeField] protected float dashSpeed = 25f;
    [SerializeField] protected float dashForce = 25f;
    bool dashing;

    [SerializeField] float dashTIme = .2f;
    float dashCounter;

    [Header("Knockback")]
    [SerializeField] float knockbackTime = .1f;
    float knockbackCounter;
    bool knockedBack;

    Vector3 knockbackVector;

    public Vector3 MoveDirection => moveDirection;

    public bool Dashing => dashing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (knockedBack)
        {
            knockbackCounter += Time.deltaTime;

            if (knockbackCounter >= knockbackTime)
            {
                knockedBack = false;
                knockbackCounter = 0f;
            }
        }
    }

    public void DashCounter()
    {
        dashCounter += Time.deltaTime;

        if (dashCounter >= dashTIme)
        {
            dashing = false;
            dashCounter = 0f;
        }
    }

    public void HandleMovement()
    {
        if (dashing)
            DashMovement();
        else if (knockedBack)
            KnockbackMovement();
        else
            NormalMovement();
    }

    private void NormalMovement()
    {
        Vector3 moveVector = moveDirection * moveForce;
        rb.AddForce(moveVector);

        yVelocity.y = rb.linearVelocity.y;

        // Clamp the rb's velocity to the magnitude of the max speed, while ensuring that y-velocity is not clamped
        velocity = rb.linearVelocity;
        velocity.y = 0f;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        rb.linearVelocity = velocity + yVelocity;
        //Debug.Log("Speed: " + velocity.magnitude);
    }

    private void DashMovement()
    {
        Vector3 moveVector = moveDirection * dashForce;
        rb.AddForce(moveVector, ForceMode.Impulse);

        yVelocity.y = rb.linearVelocity.y;

        // Clamp the rb's velocity to the magnitude of the max speed, while ensuring that y-velocity is not clamped
        velocity = rb.linearVelocity;
        velocity.y = 0f;
        velocity = Vector3.ClampMagnitude(velocity, dashSpeed);
        rb.linearVelocity = velocity + yVelocity;
        //Debug.Log("Speed: " + velocity.magnitude);
    }

    private void KnockbackMovement()
    {
        //rb.AddForce(knockbackVector, ForceMode.Impulse);
    }

    public void Move(Vector3 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    public void Dash(Vector3 dashDir)
    {
        if (dashing) return;

        dashing = true;
        moveDirection = dashDir;
        Debug.Log("Dash Direction: " + dashDir);
    }

    public void Knockback(Vector3 knockbackVector)
    {
        if (knockedBack) return;

        knockedBack = true;
        this.knockbackVector = knockbackVector;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(knockbackVector, ForceMode.Impulse);
    }
}
