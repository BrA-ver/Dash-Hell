using UnityEngine;

public class PlayerHealth : Health
{
    [Header("Invincibility")]
    [SerializeField] float invincibilityTime = 1f;
    float invincibiltyCounter;
    bool isInvincible = false;

    protected override void Update()
    {
        base.Update();

        if (invincibiltyCounter > 0f)
        {
            invincibiltyCounter -= Time.deltaTime;
            isInvincible = true;

            if (invincibiltyCounter <= 0f)
            {
                invincibiltyCounter = 0f;
                isInvincible = false;
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        if (isInvincible) return;

        invincibiltyCounter = invincibilityTime;

        base.TakeDamage(damage);
    }

    public void MakeInvincible(float duration)
    {
        invincibiltyCounter = duration;
    }
}
