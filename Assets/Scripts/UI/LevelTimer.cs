using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    private void Start()
    {
        LevelManager.instance.SetLevelTimer(this);
    }

    public void DisplayTime(float time)
    {
        ConvertToClock(time);
    }

    void ConvertToClock(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}
