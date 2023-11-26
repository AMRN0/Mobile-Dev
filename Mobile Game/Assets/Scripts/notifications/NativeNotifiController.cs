using UnityEngine;

public class NativeNotifiController : MonoBehaviour
{
    [SerializeField]
    private AndroidNotifController androidNotifController;

    // Start is called before the first frame update
    void Start()
    {
        androidNotifController.RequestAuthorisation();
        androidNotifController.RegisterNotificationChannel();
        androidNotifController.SendNotification("Come Back", "Your Wife Misses You", 10);
    }
}
