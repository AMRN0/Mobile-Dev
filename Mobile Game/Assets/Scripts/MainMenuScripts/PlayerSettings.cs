using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CandyCoded.HapticFeedback;

public class PlayerSettings : MonoBehaviour
{
    public Toggle gyroToggle;
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

        PlayerPrefs.Save();

        audioSource.volume = PlayerPrefs.GetFloat("volume");
        volumeSlider.value = audioSource.volume;

        vibrationDropdown.value = PlayerPrefs.GetInt("vibration");

        if (PlayerPrefs.GetInt("gyro") == 1)
        {
            print("on");
            gyroToggle.isOn = true;
        }
        else
        {
            print("off");
            gyroToggle.isOn = false;
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
                print("regular");
                Handheld.Vibrate();
                break;
            case 1:
                print("heavy");
                HapticFeedback.HeavyFeedback();
                break;
            case 2:
                print("medium");
                HapticFeedback.MediumFeedback();
                break;
            case 3:
                print("light");
                HapticFeedback.LightFeedback();
                break;
            default:
                break;
        }

        PlayerPrefs.Save();
    }

    public void GyroChange()
    {
        if (gyroToggle.isOn)
        {
            print("on");
            PlayerPrefs.SetInt("gyro", 1);
        }
        else
        {
            print("off");
            PlayerPrefs.SetInt("gyro", 0);
        }

        PlayerPrefs.Save();
    }
}
