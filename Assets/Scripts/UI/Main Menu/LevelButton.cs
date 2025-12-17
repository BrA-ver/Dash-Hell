using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    Button button;
    LevelSelect levelSelect;
    string targetLevel;
    [SerializeField] TextMeshProUGUI levelText;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            // Fade To Black

            // Load The target Level 
            levelSelect.GetSelectedLevel(targetLevel);
        
        });
    }

    public void SetTargetLevel(string _targetLevel)
    {
        targetLevel = _targetLevel;
        levelText.text = targetLevel;
    }

    public void GetLevelSelect(LevelSelect _levelSelect)
    {
        levelSelect = _levelSelect;
    }
}
