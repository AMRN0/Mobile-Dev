using UnityEngine;
using UnityEngine.SceneManagement;
using CandyCoded.HapticFeedback;

public class MainMenu : MonoBehaviour
{
    public GameObject quitPanel;
    public GameObject howToPlayPanel;
    public GameObject settingsPanel;

    private void Start()
    {
        Handheld.Vibrate();
        quitPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void play()
    {
        HapticFeedback.LightFeedback();
        SceneManager.LoadScene(1);
    }

    public void howToPlay()
    {
        HapticFeedback.LightFeedback();
        howToPlayPanel.SetActive(true);
    }

    public void Quit()
    {
        HapticFeedback.LightFeedback();
        quitPanel.SetActive(true);
    }

    public void QuitYes()
    {
        HapticFeedback.LightFeedback();
        Application.Quit();
    }

    public void QuitNo()
    {
        quitPanel.SetActive(false);
        HapticFeedback.LightFeedback();
    }

    public void HowToPlayBack()
    {
        HapticFeedback.LightFeedback();
        howToPlayPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void settings()
    {
        HapticFeedback.LightFeedback();
        settingsPanel.SetActive(true);
    }
}