using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] WaveManager[] waveManagers;
    int completeBattles;

    private void Start()
    {
        waveManagers = FindObjectsByType<WaveManager>(FindObjectsSortMode.InstanceID);
        foreach (WaveManager manager in waveManagers)
        {
            manager.OnBattleEnded.AddListener(OnBattleEnded);
        }

        AudioManager.Instance.PlayMusic("Level");
    }

    private void OnBattleEnded()
    {
        completeBattles++;

        if (completeBattles >= waveManagers.Length)
        {
            GameEvents.Instance.Victory();
        }
    }
}
