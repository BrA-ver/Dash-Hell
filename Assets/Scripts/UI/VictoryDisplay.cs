using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VictoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject victoryScreen;

    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] Button nextLevelButton;
    [SerializeField] Button replayButton;
    [SerializeField] Button mainMenuButton;

    private void Start()
    {
        GameEvents.Instance.OnVictory.AddListener(OnVictory);

        nextLevelButton.onClick.AddListener(NextLevel);
    }

    private void OnVictory()
    {
        ShowVictoryScreen();
    }

    public void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);

        ConvertToClock(LevelManager.instance.LevelTime);
    }

    void ConvertToClock(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    public void NextLevel()
    {
        LevelManager.instance.NextLevel();
    }

    public void Retry()
    {
        LevelManager.instance.Retry();
    }

    public void MainMenu()
    {
        GameManager.Instance.MainMenu();
    }
}
