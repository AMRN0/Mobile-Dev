using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PhoneCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

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
    }

    [ContextMenu("Take A PIC")]
    public void TakePic()
    {
        StartCoroutine(TakePhoto());
    }

    private string[] picName = { "pic0", "pic1", "pic2", "pic3", "pic4" };
    private int count = 0;

    IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(backCam.width, backCam.height);
        photo.SetPixels(backCam.GetPixels());
        photo.Apply();

        byte[] bytes = photo.EncodeToPNG();

        File.WriteAllBytes("Assets/Resources/textures/" + picName[count] + ".png", bytes);

        count++;
    }
}
