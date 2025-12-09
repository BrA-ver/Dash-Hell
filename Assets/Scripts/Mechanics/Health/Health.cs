using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 10;
    [SerializeField] protected int currentHealth;

    public UnityEvent<int> OnhealthChange;
    public UnityEvent OnTookDamage;
    public UnityEvent OnDied;

    [SerializeField] GameObject deathEffect;

    public float HealthRatio { get { return (float)currentHealth / (float)maxHealth; } }

    protected void Start()
    {
        currentHealth = maxHealth;
        OnhealthChange?.Invoke(currentHealth);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnTookDamage?.Invoke();
        OnhealthChange?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        currentHealth = 0;
        OnhealthChange?.Invoke(currentHealth);
        OnDied?.Invoke();
        Instantiate(deathEffect, transform.position, Quaternion.identity);
    }
}
