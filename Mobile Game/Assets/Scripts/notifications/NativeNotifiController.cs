using UnityEngine;

public class NativeNotifiController : MonoBehaviour
{
    [SerializeField]
    private AndroidNotifController androidNotifController;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        androidNotifController.RegisterNotificationChannel();
#endif

        Application.quitting += Quitting;
    }

   
    void Quitting()
    {
        print("Application Quitting");
        androidNotifController.SendNotification("Come Back", "You need to beat your highscore", 600);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus) return;

        Quitting();
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause) return;

        Quitting();
    }

    private void OnApplicationQuit()
    {
        Quitting();
    }
}