using System;
using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    [Header("Player Death")]
    [SerializeField] float waitAfterDeath = 1f;
    bool handlingDeath;

    private void Start()
    {
        GameEvents.Instance.OnPlayerDied.AddListener(HandlePlayerDeath);
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnPlayerDied.RemoveListener(HandlePlayerDeath);
    }

    private void HandlePlayerDeath()
    {
        if (handlingDeath) return;
        handlingDeath = true;
        StartCoroutine(playerDeathRoutine());
    }

    IEnumerator playerDeathRoutine()
    {
        yield return new WaitForSeconds(waitAfterDeath);
        handlingDeath = false;
        GameEvents.Instance.GameOver();
    }
}
