using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Image BG, BG2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameEvents.Instance.OnGameOver.AddListener(OnGameOver);
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnGameOver.RemoveListener(OnGameOver);
        
    }

    private void OnGameOver()
    {
        ShowGameOver();
    }

    private void ShowGameOver()
    {
        BG.gameObject.SetActive(true);
        BG2.gameObject.SetActive(true);
    }
}
