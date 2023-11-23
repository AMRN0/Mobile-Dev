using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using NativeGalleryNamespace;

public class PhoneCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

    private int count = 0;

    //NativeGallery.MediaPickCallback callback;
    private string[] picName = { "pic0", "pic1", "pic2", "pic3" };
    public Material[] materials;

    private void Awake()
    {
        NativeGallery.Permission readPermission = NativeGallery.CheckPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);
        NativeGallery.Permission writePermission = NativeGallery.CheckPermission(NativeGallery.PermissionType.Write, NativeGallery.MediaType.Image);

        if (readPermission == NativeGallery.Permission.Denied || writePermission == NativeGallery.Permission.Denied)
        {
            SceneManager.LoadScene(2);
            return;
        }
        else if (readPermission == NativeGallery.Permission.ShouldAsk || writePermission == NativeGallery.Permission.ShouldAsk)
        {
            NativeGallery.RequestPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);
            NativeGallery.RequestPermission(NativeGallery.PermissionType.Write, NativeGallery.MediaType.Image);
        }
    }

    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            print("no devices detected");
            camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
                break;
            }
            else
            {
                backCam = new WebCamTexture(devices[0].name, Screen.width, Screen.height);
            }
        }

        if (backCam == null)
        {
            print("back cam doesnt exist");
            return;
        }

        backCam.Play();

        background.texture = backCam;

        camAvailable = true;
    }

    private void Update()
    {
        if (!camAvailable)
        {
            return;
        }

        float ratio = (float)backCam.width / (float)backCam.height;

        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1.0f : 1.0f;

        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;

        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

        if (count >= 4)
        {
            NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((callback) =>
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    Texture2D texture = NativeGallery.LoadImageAtPath(callback, 2);

                    materials[i].mainTexture = texture;
                }

            }, "Select image to use as texture", "image/*");

            if (permission == NativeGallery.Permission.Granted)
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    [ContextMenu("Take A PIC")]
    public void TakePic()
    {

        if (count >= 4)
        {
            return;
        }

        StartCoroutine(TakePhoto());
    }

    IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(backCam.width, backCam.height);
        photo.SetPixels(backCam.GetPixels());
        photo.Apply();

        NativeGallery.SaveImageToGallery(photo, "game", picName[count]);

        count++;
    }
}
