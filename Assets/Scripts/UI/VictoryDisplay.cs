using System;
using UnityEngine;

public class VictoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject victoryScreen;

    private void Start()
    {
        GameEvents.Instance.OnVictory.AddListener(OnVictory);
    }

    private void OnVictory()
    {
        ShowVictoryScreen();
    }

    public void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
    }
}
