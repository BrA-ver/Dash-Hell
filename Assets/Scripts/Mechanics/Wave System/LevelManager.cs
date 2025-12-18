using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] WaveManager[] waveManagers;
    int completeBattles;

    float levelTime = 0f;
    LevelTimer levelTimer;
    bool levelComplete;

    [Header("Levels")]
    [SerializeField] string nextLevel;

    public float LevelTime => levelTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        waveManagers = FindObjectsByType<WaveManager>(FindObjectsSortMode.InstanceID);
        foreach (WaveManager manager in waveManagers)
        {
            manager.OnBattleEnded.AddListener(OnBattleEnded);
        }

        AudioManager.Instance.PlayMusic("Level");
        GameManager.Instance.PlayerDied = false;
    }

    private void Update()
    {
        if (levelComplete) return;
        levelTime += Time.deltaTime;
        levelTimer.DisplayTime(levelTime);
    }

    private void OnBattleEnded()
    {
        completeBattles++;

        if (completeBattles >= waveManagers.Length)
        {
            GameEvents.Instance.Victory();
            levelComplete = true;
        }
    }

    public void SetLevelTimer(LevelTimer timer)
    {
        levelTimer = timer;
    }

    public void NextLevel()
    {
        GameManager.Instance.LoadLevel(nextLevel);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
