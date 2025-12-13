using UnityEngine;

[RequireComponent(typeof(Movement), typeof(GroundDetector))]
public class Character : MonoBehaviour
{
    protected Movement movement;
    protected Health health;
    protected GroundDetector groundDetector;

    public Movement Movement => movement;
    public Health Health => health;
    public GroundDetector GroundDetector => groundDetector;

    protected virtual void Awake()
    {
        movement = GetComponent<Movement>();
        health = GetComponent<Health>();
        groundDetector = GetComponent<GroundDetector>();
    }

    protected virtual void Start(){}

    protected virtual void Update(){}

    protected virtual void FixedUpdate()
    {
        BoostGravityWhenOffGround();
        movement.HandleMovement();
    }

    protected virtual void BoostGravityWhenOffGround()
    {
        if (!groundDetector.OnGround)
            movement.BoostGravity();
    }
}
