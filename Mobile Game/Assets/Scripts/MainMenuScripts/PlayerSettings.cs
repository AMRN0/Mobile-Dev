using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CandyCoded.HapticFeedback;

public class PlayerSettings : MonoBehaviour
{
    public Toggle gyroToggle;
    public Toggle picToggle;
    public TMP_Dropdown vibrationDropdown;
    public Slider volumeSlider;

    public AudioSource audioSource;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1);
        }

        if (!PlayerPrefs.HasKey("vibration"))
        {
            PlayerPrefs.SetInt("vibration", 0);
        }

        if (!PlayerPrefs.HasKey("gyro"))
        {
            PlayerPrefs.SetInt("gyro", 1);
        }

        if (!PlayerPrefs.HasKey("picture"))
        {
            PlayerPrefs.SetInt("picture", 1);
        }

        PlayerPrefs.Save();

        audioSource.volume = PlayerPrefs.GetFloat("volume");
        volumeSlider.value = audioSource.volume;

        vibrationDropdown.value = PlayerPrefs.GetInt("vibration");

        if (PlayerPrefs.GetInt("gyro") == 1)
        {
            gyroToggle.isOn = true;
        }
        else
        {
            gyroToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt("picture") == 1)
        {
            picToggle.isOn = true;
        }
        else
        {
            picToggle.isOn = false;
        }
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
                Handheld.Vibrate();
                break;
        }

        PlayerPrefs.Save();
    }

    public void GyroChange()
    {
        if (gyroToggle.isOn)
        {
            PlayerPrefs.SetInt("gyro", 1);
        }
        else
        {
            PlayerPrefs.SetInt("gyro", 0);
        }

        PlayerPrefs.Save();
    }

    public void TakePicture()
    {
        if (picToggle.isOn)
        {
            PlayerPrefs.SetInt("picture", 1);
        }
        else
        {
            PlayerPrefs.SetInt("picture", 0);
        }

        PlayerPrefs.Save();
    }
}
