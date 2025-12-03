using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 10;
    [SerializeField] protected int currentHealth;

    public float HealthRatio { get { return (float)currentHealth / (float)maxHealth; } }

    protected void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
