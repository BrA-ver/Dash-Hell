using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Image BG, BG2;

    [SerializeField] Button retryButton, quitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameEvents.Instance.OnGameOver.AddListener(OnGameOver);

        retryButton.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        quitButton.onClick.AddListener(() => {
            GameManager.Instance.MainMenu();
        });
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
