using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button play, settings, quit;

    [SerializeField] string targetScene;

    private void Start()
    {
        play.onClick.AddListener(Play);
        settings.onClick.AddListener(Settings);
        quit.onClick.AddListener(Quit);
    }

    void Play()
    {
        SceneManager.LoadScene(targetScene);
    }

    void Settings()
    {

    }

    void Quit()
    {
        Application.Quit();
    }
}
