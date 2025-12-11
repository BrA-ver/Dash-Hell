using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Player Death")]
    [SerializeField] float waitAfterDeath = 1f;
    bool handlingDeath;

    [SerializeField] string mainMenu = "MainMenu";

    private void Start()
    {
        GameEvents.Instance.OnPlayerDied.AddListener(HandlePlayerDeath);
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnPlayerDied.RemoveListener(HandlePlayerDeath);
    }

    #region Player Death
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
    #endregion

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
