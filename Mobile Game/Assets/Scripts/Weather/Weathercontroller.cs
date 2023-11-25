using System.Collections;
using System.Net;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;


[SerializeField]
public class Weather
{
    public int id;
    public string main;

}

[SerializeField]
public class WeatherInfo
{
    public int id;
    public int name;
    public List<Weather> weather;
}




public class Weathercontroller : MonoBehaviour
{
    private const string API_KEY = "6902c2b5343c96b279c56b839be4cef4";
    public float longitude, latitude;
    //private const string url = "api.openweathermap.org/data/2.5/weather?";

    [SerializeField] TextMeshProUGUI text;

    public GameObject snowSystem;

    bool snowing = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            text.text = "Location not enabled on device or app does not have permission to access location";
            print("Location not enabled on device or app does not have permission to access location");

            latitude = 52.44f;
            longitude = 1.85f;
            CheckSnowStatus();
            return;
        }
        StartCoroutine(GetLatLong());

        CheckSnowStatus();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckSnowStatus();
    }

    void CheckSnowStatus()
    {
        bool snowing = GetWeather().weather[0].main.Equals("Snow");
        print(GetWeather().weather[0].main);
        //snowing = GetWeather().weather[0].main.Equals(GetWeather().weather[0].main);

        if (snowing)
        {
            snowSystem.SetActive(true);
        }
        else
        {
            snowSystem.SetActive(false);
        }
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

    private WeatherInfo GetWeather()
    {
        //UnityWebRequest req = UnityWebRequest.Get(string.Format("https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}", longitude, latitude, API_KEY));
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}", longitude, latitude, API_KEY));
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?lat=52.44&lon=1.85&appid=6902c2b5343c96b279c56b839be4cef4");

        WeatherInfo info;

        //switch (req.result)
        //{
        //    case UnityWebRequest.Result.InProgress:
        //        break;
        //    case UnityWebRequest.Result.Success:
        //        info = req.downloadHandler.data.GetValue(0);
        //        break;
        //    case UnityWebRequest.Result.ConnectionError:
        //        break;
        //    case UnityWebRequest.Result.ProtocolError:
        //        break;
        //    case UnityWebRequest.Result.DataProcessingError:
        //        break;
        //    default:
        //        break;
        //}

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        StreamReader reader = new(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);

        return info;
    }
}
