using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using CandyCoded.HapticFeedback;

public class GameUI : MonoBehaviour
{
    public GameObject pausePanel;

    public TMP_Dropdown vibrationDropdown;
    public Slider volumeSlider;

    public AudioSource audioSource;


    private void Awake()
    {
        pausePanel.SetActive(false);

        audioSource.volume = PlayerPrefs.GetFloat("volume");
        volumeSlider.value = audioSource.volume;

        vibrationDropdown.value = PlayerPrefs.GetInt("vibration");
    }

    public void pauseClick()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(!pausePanel.activeSelf);

        if (pausePanel.activeSelf) return;

        Time.timeScale = 1;
    }

    public void SliderChange()
    {
        audioSource.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", volumeSlider.value);

        PlayerPrefs.Save();
    }

    public void DropdownChange()
    {
        PlayerPrefs.SetInt("vibration", vibrationDropdown.value);

        switch (vibrationDropdown.value)
        {
            case 0:
                Handheld.Vibrate();
                break;
            case 1:
                HapticFeedback.HeavyFeedback();
                break;
            case 2:
                HapticFeedback.MediumFeedback();
                break;
            case 3:
                HapticFeedback.LightFeedback();
                break;
            default:
                break;
        }

        PlayerPrefs.Save();
    }

    public void MainMenu()
    {
        PlayerPrefs.Save();
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}
