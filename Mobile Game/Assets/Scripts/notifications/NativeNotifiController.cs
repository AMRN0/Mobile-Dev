using UnityEngine;

public class NativeNotifiController : MonoBehaviour
{
    [SerializeField]
    private AndroidNotifController androidNotifController;

    // Start is called before the first frame update
    void Start()
    {
        androidNotifController.RegisterNotificationChannel();
        Application.quitting += Quitting;
    }

   
    void Quitting()
    {
        print("Application Quitting");
        androidNotifController.SendNotification("Come Back", "You need to beat your highscore", 10);
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
}
