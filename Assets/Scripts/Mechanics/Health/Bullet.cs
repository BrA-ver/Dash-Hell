using UnityEngine;

public class Bullet : Hurtbox
{
    [SerializeField] protected float lifetime = 1f;

    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, lifetime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        //Destroy(gameObject);
    }
}
