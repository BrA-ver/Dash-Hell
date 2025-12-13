using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Wave> waves = new List<Wave>();
    int currentWave;
    bool battleStarted;

    public UnityEvent OnBattleStarted;
    public UnityEvent OnBattleEnded;

    private void Start()
    {
        GetChildrenAsWaves();
    }

    private void OnDisable()
    {
        if (!battleStarted) return;

        foreach (Wave wave in waves)
        {
            wave.OnWaveEnded.RemoveListener(OnWaveEnded);
        }
    }

    private void GetChildrenAsWaves()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out Wave wave))
            {
                waves.Add(wave);
                wave.OnWaveEnded.AddListener(OnWaveEnded);
            }
        }
    }

    public void StartBattle()
    {
        StartWave();

        battleStarted = true;
        OnBattleStarted?.Invoke();
    }

    private void StartWave()
    {
        Wave _currentWave = waves[currentWave];
        _currentWave.StartWave();
    }

    private void OnWaveEnded()
    {
        currentWave++;

        if (currentWave >= waves.Count)
        {
            // End The Battle
            OnBattleEnded?.Invoke();
            return;
        }

        StartWave();
    }
}
