using UnityEngine;

public class VictoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject victoryScreen;

    public void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
    }
}
