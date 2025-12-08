using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Character : MonoBehaviour
{
    protected Movement movement;
    protected Health health;

    public Movement Movement => movement;

    protected virtual void Awake()
    {
        movement = GetComponent<Movement>();
        health = GetComponent<Health>();
    }

    protected virtual void Start(){}

    protected virtual void Update(){}

    protected virtual void FixedUpdate()
    {
        movement.HandleMovement();
    }
}
