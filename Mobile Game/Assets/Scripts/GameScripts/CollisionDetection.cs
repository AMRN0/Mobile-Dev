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
                print("regular");
                mEvent.AddListener(defaultFeedback);
                break;
            case 1:
                print("heavy");
                mEvent.AddListener(HapticFeedbackHard);
                break;
            case 2:
                print("medium");
                mEvent.AddListener(HapticFeedbackMedium);
                break;
            case 3:
                print("light");
                mEvent.AddListener(HapticFeedbackLight);
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mEvent.AddListener(HapticFeedbackHard);   
    }

    // Update is called once per frame
    void Update()
    {
        
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

        print(livesRemaining);

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
        SceneManager.LoadScene(2);
    }
}