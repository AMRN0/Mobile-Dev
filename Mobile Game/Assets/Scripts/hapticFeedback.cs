using UnityEngine;
using CandyCoded.HapticFeedback;
using UnityEngine.UI;

public class hapticFeedback : MonoBehaviour
{

   
    [SerializeField]
    private Button regularButton;

    [SerializeField]
    private Button HardButton;

    [SerializeField]
    private Button MediumButton;

    [SerializeField]
    private Button LightButton;

    private void OnEnable()
    {
        regularButton.onClick.AddListener(defaultFeedback);
        HardButton.onClick.AddListener(HapticFeedbackHard);
        MediumButton.onClick.AddListener(HapticFeedbackMedium);
        LightButton.onClick.AddListener(HapticFeedbackLight);
    }

    private void OnDisable()
    {
        regularButton.onClick.RemoveListener(defaultFeedback);
        HardButton.onClick.RemoveListener(HapticFeedbackHard);
        MediumButton.onClick.RemoveListener(HapticFeedbackMedium);
        LightButton.onClick.RemoveListener(HapticFeedbackLight);
    }

    void HapticFeedbackHard()
    {
        HapticFeedback.HeavyFeedback();
    }
    void HapticFeedbackMedium()
    {
        HapticFeedback.MediumFeedback();
        
    }
    void HapticFeedbackLight()
    {
        HapticFeedback.LightFeedback();
    }

    void defaultFeedback()
    {
        Handheld.Vibrate();
    }
}
