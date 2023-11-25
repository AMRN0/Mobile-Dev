using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;
using System;

public class APICaller : MonoBehaviour
{

    bool routine;
    private const string url = "api.openweathermap.org/data/2.5/weather?";
    private const string apiKey = "6902c2b5343c96b279c56b839be4cef4";

    [SerializeField] TextMeshProUGUI text;

    [SerializeField] int currentAPIIndex;

    private float latitude, longitude;

    private void Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            text.text = "Location not enabled on device or app does not have permission to access location";
            print("Location not enabled on device or app does not have permission to access location");

            latitude = 52.44f;
            longitude = 1.85f;

            return;
        }
        StartCoroutine(GetLatLong());

        //Input.location.Start();
    }

    IEnumerator GetLatLong()
    {
        Input.location.Start();

        int maxWait = 10;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait < 1)
        {
            text.text = "Failed to Init location in 10 seconds";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            text.text = "Failed to Initialise";
            yield break;
        }
        else
        {
            latitude = (float)Input.location.lastData.latitude;
            longitude = (float)Input.location.lastData.longitude;

            text.text = "lat: " + latitude + " long: " + longitude;

            if (latitude.ToString() != "0")
            {
                Input.location.Stop();
                StopCoroutine("Start");
                text.text = Input.location.status + "lat: " + latitude + " long: " + longitude;
            }
        }
        Input.location.Stop();
    }

    private void OnMouseDown()
    {
        RequestAPI();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            RequestAPI();
        }
    }

    public void RequestAPI()
    {
        if (!routine) StartCoroutine(WaitForServerResponse());

    }

    IEnumerator WaitForServerResponse()
    {
        routine = true;
        UnityWebRequest req = UnityWebRequest.Get(url);

        if (currentAPIIndex == 1)
        {
            string weatherURL = url + "lat=" + latitude + "&lon=" + longitude + "&appid=" + apiKey;
            print(weatherURL);
            text.text = weatherURL;
            req = UnityWebRequest.Get(weatherURL);
        }
        else req = UnityWebRequest.Get(url);

        string[] pages = req.url.Split('/');
        int page = pages.Length - 1;
        yield return req.SendWebRequest();

        print(currentAPIIndex);

        switch (req.result)
        {

            case UnityWebRequest.Result.ConnectionError:
                text.text = "connection error";
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(pages[page] + ": Error: " + req.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(pages[page] + ": HTTP Error: " + req.error + " " + name);
                break;
            case UnityWebRequest.Result.Success:

                switch (currentAPIIndex)
                {
                    case 0:
                        text.text = req.downloadHandler.text;
                        break;
                    case 1:
                        JSONNode info = JSON.Parse(req.downloadHandler.text);
                        string s = info["weather"].ToString().Substring(info["weather"].ToString().IndexOf("main") + 7, 10);
                        text.text = s.Substring(0, s.IndexOf("\""));

                        break;
                    case 2:
                        text.text = "huh?";

                        break;

                }
                break;
            default:
                break;
        }


        routine = false;

    }

}