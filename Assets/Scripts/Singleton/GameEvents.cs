using UnityEngine;
using UnityEngine.Events;

public class GameEvents : Singleton<GameEvents>
{
    public UnityEvent OnPlayerDied;
    public UnityEvent OnGameOver;

    public void PlayerDied()
    {
        OnPlayerDied?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }
}