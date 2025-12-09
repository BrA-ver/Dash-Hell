using UnityEngine;
using UnityEngine.Events;

public class GameEvents : Singleton<GameEvents>
{
    public UnityEvent OnPlayerDied;
    public UnityEvent OnPlayerTookDamage;
    public UnityEvent OnGameOver;
    public UnityEvent<int> OnPlayerHealthChanged;

    public void PlayerDied()
    {
        OnPlayerDied?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void PlayerHealthChanged(int currentHealth)
    {
        OnPlayerHealthChanged?.Invoke(currentHealth);
    }

    public void PlayerTookDamage()
    {
        OnPlayerTookDamage?.Invoke();
    }
}