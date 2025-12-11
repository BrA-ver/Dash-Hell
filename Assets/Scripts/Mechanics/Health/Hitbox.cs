using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] Health health;
    [field: SerializeField] public HitboxType Type { get; private set; }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }
}
