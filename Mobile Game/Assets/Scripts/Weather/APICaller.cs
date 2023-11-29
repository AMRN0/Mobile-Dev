using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;

public class APICaller : MonoBehaviour
{
    bool routine;
    private const string url = "api.openweathermap.org/data/2.5/weather?";
    private const string apiKey = "6902c2b5343c96b279c56b839be4cef4";

    [SerializeField] TextMeshProUGUI text;

    private double latitude, longitude;

    [SerializeField] GameObject[] conditions;

    private void Awake()
    {
        if (!Input.location.isEnabledByUser)
        {
            text.text = "Location not enabled on device or app does not have permission to access location";
            print("Location not enabled on device or app does not have permission to access location");

            latitude = 52.4862;
            longitude = -1.8904;

            return;
        }
        StartCoroutine(GetLatLong());
    }


    private void Start()
    {
        //if (!Input.location.isEnabledByUser)
        //{
        //    return;
        //}
        RequestAPI();
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

            text.text = latitude + "," + longitude + "," + Input.location.lastData.horizontalAccuracy + "," + Input.location.lastData.verticalAccuracy;

            if (latitude.ToString() != "0")
            {
                Input.location.Stop();
                StopCoroutine("Start");
                text.text = Input.location.status + "lat: " + latitude + " long: " + longitude;
            }
        }
        Input.location.Stop();
    }

    public void RequestAPI()
    {
        if (!routine) StartCoroutine(WaitForServerResponse());

    }

    IEnumerator WaitForServerResponse()
    {
        routine = true;
        UnityWebRequest req;

        string weatherURL = url + "lat=" + latitude + "&lon=" + longitude + "&appid=" + apiKey;
        print(weatherURL);
        text.text = weatherURL;
        req = UnityWebRequest.Get(weatherURL);

        string[] pages = req.url.Split('/');
        int page = pages.Length - 1;
        yield return req.SendWebRequest();

        switch (req.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                text.text = "connection error";
                conditions[1].SetActive(true);
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(pages[page] + ": Error: " + req.error);
                conditions[1].SetActive(true);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(pages[page] + ": HTTP Error: " + req.error + " " + name);
                conditions[1].SetActive(true);
                break;
            case UnityWebRequest.Result.Success:
                JSONNode info = JSON.Parse(req.downloadHandler.text);
                string s = info["weather"].ToString().Substring(info["weather"].ToString().IndexOf("main") + 7, 10);
                text.text = s.Substring(0, s.IndexOf("\""));

                print("The location is: " + info["weather"]["0"]["main"]);

                //text.text = latitude + "," + longitude + "," + Input.location.lastData.horizontalAccuracy + "," + Input.location.lastData.verticalAccuracy;

                switch (text.text)
                {
                    case "Thunderstorm":
                        conditions[0].SetActive(true);
                        break;
                    case "Drizzle":
                        conditions[1].SetActive(true);
                        break;
                    case "Rain":
                        conditions[2].SetActive(true);
                        break;
                    case "Clouds":
                        conditions[3].SetActive(true);
                        break;
                    case "Clear":
                        for (int i = 0; i < conditions.Length; i++)
                        {
                            conditions[i].SetActive(false);
                        }
                        break;
                    case "Snow":
                        conditions[4].SetActive(true);
                        break;
                    case "Mist":
                        conditions[5].SetActive(true);
                        break;
                    case "Dust":
                        conditions[5].SetActive(true);
                        break;
                    case "Smoke":
                        conditions[6].SetActive(true);
                        break;
                    case "Haze":
                        conditions[5].SetActive(true);
                        break;
                    case "Fog":
                        conditions[5].SetActive(true);
                        break;
                    case "Sand":
                        conditions[7].SetActive(true);
                        break;
                    case "Ash":
                        conditions[6].SetActive(true);
                        break;
                    case "Squall":
                        conditions[0].SetActive(true);
                        conditions[2].SetActive(true);
                        conditions[4].SetActive(true);
                        conditions[5].SetActive(true);
                        break;
                    case "Tornado":
                        for (int i = 0; i < conditions.Length; i++)
                        {
                            conditions[i].SetActive(true);
                        }
                        break;
                    default:
                        conditions[1].SetActive(true);
                        break;
                }
                break;
            default:
                break;
        }
        routine = false;
    }
}