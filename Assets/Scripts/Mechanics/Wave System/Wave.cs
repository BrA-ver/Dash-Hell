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

    private void Awake()
    {
        GetAllChildrenAsEnemies();
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
}
