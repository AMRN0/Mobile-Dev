using UnityEngine;
using UnityEngine.SceneManagement;
using CandyCoded.HapticFeedback;

public class MainMenu : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject settingsPanel;

    private void Start()
    {
        Handheld.Vibrate();
        howToPlayPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void HowToPlayBack()
    {
        HapticFeedback.LightFeedback();
        howToPlayPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void SettingsContinue()
    {
        HapticFeedback.LightFeedback();
        settingsPanel.SetActive(false);
        if (PlayerPrefs.GetInt("picture") == 1)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
        
    }
}