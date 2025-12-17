using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{
    [SerializeField] string[] levels;
    [SerializeField] LevelButton levelButton;
    [SerializeField] Transform buttonHolder;

    [SerializeField] Button selectButton;

    string selectedLevel;

    private void Awake()
    {
        selectButton.onClick.AddListener(() => {
            if (selectedLevel != string.Empty)
            {
                SceneManager.LoadScene(selectedLevel);
            }

        });
    }

    private void Start()
    {
        ShowLevelButtons();
    }

    private void ShowLevelButtons()
    {
        foreach (string level in levels)
        {
            LevelButton newButton = Instantiate(levelButton, buttonHolder);
            newButton.SetTargetLevel(level);
            newButton.GetLevelSelect(this);
        }
    }

    public void GetSelectedLevel(string _selectedLevel)
    {
        selectedLevel = _selectedLevel;
    }
}
