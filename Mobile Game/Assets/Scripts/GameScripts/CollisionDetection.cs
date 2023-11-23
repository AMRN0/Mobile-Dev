using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using CandyCoded.HapticFeedback;

public class CollisionDetection : MonoBehaviour
{

    public Image[] liveImage;
    public int livesRemaining = 3;

    UnityEvent mEvent = new UnityEvent();

    private void Awake()
    {
        switch (PlayerPrefs.GetInt("vibration"))
        {
            case 0:
                mEvent.AddListener(defaultFeedback);
                break;
            case 1:
                mEvent.AddListener(HapticFeedbackHard);
                break;
            case 2:
                mEvent.AddListener(HapticFeedbackMedium);
                break;
            case 3:
                mEvent.AddListener(HapticFeedbackLight);
                break;
            default:
                break;
        }
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

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);

        mEvent.Invoke();

        LoseLive();
    }

    [ContextMenu("Lose life")]
    public void LoseLive()
    {
        livesRemaining--;

        if (livesRemaining <= 0)
        {
            Destroy(this.gameObject);
        }
        
        liveImage[livesRemaining].enabled = false;
    }

    private void OnDestroy()
    {
        mEvent.RemoveAllListeners();
        SceneManager.LoadScene(3);
    }
}