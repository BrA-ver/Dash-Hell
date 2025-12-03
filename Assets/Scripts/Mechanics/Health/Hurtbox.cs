using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Hitbox hitbox))
        {
            hitbox.TakeDamage(damage);
        }
    }
}
