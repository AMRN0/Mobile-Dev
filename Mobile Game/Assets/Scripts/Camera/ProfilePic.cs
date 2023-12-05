using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePic : MonoBehaviour
{
    [SerializeField]
    private RawImage Image;

    private void Awake()
    {
        NativeGallery.Permission readPermission = NativeGallery.CheckPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);
       
        if (readPermission == NativeGallery.Permission.Denied)
        {
            return;
        }
        else if (readPermission == NativeGallery.Permission.ShouldAsk)
        {
            NativeGallery.RequestPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);
        }

        if (PlayerPrefs.GetString("ProfilePic") != null)
        {
            GetProfilePic();
        }
    }

    public void SetProfilePic()
    {
        NativeGallery.GetImageFromGallery((callback) =>
        {
            Texture2D texture = NativeGallery.LoadImageAtPath(callback, 2, true, false, true);

            PlayerPrefs.SetString("ProfilePic", callback);
            Image.texture = texture;

        }, "Select image to use as profile picture", "image/*");

    }

    void GetProfilePic()
    {
        Texture2D texture = NativeGallery.LoadImageAtPath(PlayerPrefs.GetString("ProfilePic"), 2);
        Image.texture = texture;
        return;
    }
}
