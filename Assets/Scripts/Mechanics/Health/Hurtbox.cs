using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] int damage;
    [field: SerializeField] public HitboxType TargetType { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.TryGetComponent(out Hitbox hitbox))
        {
            if (hitbox.Type != TargetType) return;

            hitbox.TakeDamage(damage);
        }
    }
}

public enum HitboxType
{
    Player,
    Enemy
}
