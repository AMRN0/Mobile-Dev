#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;

public class AndroidNotifController : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void RequestAuthorisation()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    public void RegisterNotificationChannel()
    {
        AndroidNotificationChannel channel = new()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic Notifications"
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(string title, string text, int fireTimeInSeconds)
    {
        AndroidNotification notification = new();

        notification.Title = title;
        notification.Text = text;
        notification.FireTime = System.DateTime.Now.AddSeconds(fireTimeInSeconds);

        AndroidNotificationCenter.SendNotification(notification, channelId: "default_channel");
    }
}
#endif