using UnityEngine;

public class NativeNotifiController : MonoBehaviour
{
    [SerializeField]
    private AndroidNotifController androidNotifController;

    // Start is called before the first frame update
    void Start()
    {
        Application.quitting += Quitting;
    }

   
    void Quitting()
    {
        androidNotifController.RegisterNotificationChannel();
        androidNotifController.SendNotification("Come Back", "You need to beat your highscore", 10);
    }
}
