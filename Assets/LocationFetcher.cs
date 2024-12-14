using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LocationFetcher : MonoBehaviour
{
    public string apiKey = "AIzaSyBeuY4Zwi0eslU4NBBcHIovxrx4cWIcib0"; //my api key
    public string addressToGeocode = "";

    public void FetchCoordinates()
    {
        StartCoroutine(GetCoordinates(addressToGeocode));
    }

    IEnumerator GetCoordinates(string address)
    {
        string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={UnityWebRequest.EscapeURL(address)}&key={apiKey}";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                var results = JsonUtility.FromJson<GeocodeResponse>(webRequest.downloadHandler.text);
                if (results.status == "OK" && results.results.Length > 0)
                {
                    Vector2 destination = new Vector2(results.results[0].geometry.location.lat, results.results[0].geometry.location.lng);
                    // Use the destination coordinates here
                }
                else
                {
                    Debug.LogError("Geocoding failed: " + results.status);
                }
            }
            else
            {
                Debug.LogError("Error: " + webRequest.error);
            }
        }
    }

    [System.Serializable]
    public class GeocodeResponse
    {
        public string status;
        public Result[] results;
    }

    [System.Serializable]
    public class Result
    {
        public Geometry geometry;
    }

    [System.Serializable]
    public class Geometry
    {
        public Location location;
    }

    [System.Serializable]
    public class Location
    {
        public float lat;
        public float lng;
    }
}