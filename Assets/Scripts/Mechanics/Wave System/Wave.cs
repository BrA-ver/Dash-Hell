using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using System;

public class Wave : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies = new List<Enemy>();
    [SerializeField] GameObject spawnEffect;
    [SerializeField] float effectWaitTime = .3f;

    public UnityEvent OnWaveStarted;
    public UnityEvent OnWaveEnded;

    int deadEnemies = 0;
    bool waveStarted = false;
    bool playerDied;

    private void Awake()
    {
        GetAllChildrenAsEnemies();
    }

    private void Start()
    {
        GameEvents.Instance.OnPlayerDied.AddListener(OnPlayerDied);
    }

    private void OnDestroy()
    {
        if (!waveStarted) return;
        foreach (Enemy enemy in enemies)
        {
            enemy.Health.OnDied.RemoveListener(OnEnemyDied);
        }
    }

    public void StartWave()
    {
        if (playerDied) return;

        waveStarted = true;
        StartCoroutine(SpawnEnemiesRoutine());
    }

    void SpawnEnemy(Enemy enemy)
    {
        // Activate Enemy
        enemy.gameObject.SetActive(true);
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        foreach (Enemy enemy in enemies)
        {
            // Show Spawn Effect
            Instantiate(spawnEffect, enemy.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(effectWaitTime);

            // Wait For the Effect to finish
            SpawnEnemy(enemy);
        }
    }

    private void GetAllChildrenAsEnemies()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out Enemy enemy))
            {
                enemies.Add(enemy);
                enemy.GetComponent<Health>().OnDied.AddListener(OnEnemyDied);
                Debug.Log("Add");
            }
        }
    }

    private void OnEnemyDied()
    {
        deadEnemies++;
        bool waveOver = deadEnemies >= enemies.Count;

        if (waveOver)
        {
            OnWaveEnded?.Invoke();
            waveStarted = false;
        }
    }

    #region Game Events
    void OnPlayerDied()
    {
        playerDied = true;
        StopCoroutine(SpawnEnemiesRoutine());
    }
    #endregion
}
