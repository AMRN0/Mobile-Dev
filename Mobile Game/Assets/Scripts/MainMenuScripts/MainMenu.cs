using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject quitPanel;
    public GameObject howToPlayPanel;
    public GameObject settingsPanel;

    private void Start()
    {
        quitPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }

    public void howToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    public void Quit()
    {
        quitPanel.SetActive(true);
    }

    public void QuitYes()
    {
        Application.Quit();
    }

    public void QuitNo()
    {
        quitPanel.SetActive(false);
    }

    public void HowToPlayBack()
    {
        howToPlayPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void settings()
    {
        settingsPanel.SetActive(true);
    }
}
