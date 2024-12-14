using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleMapsService : MonoBehaviour
{
    private const string API_KEY = "AIzaSyBeuY4Zwi0eslU4NBBcHIovxrx4cWIcib0"; // Replace with your API Key
    public string destinationAddress;
    public float destinationLat;
    public float destinationLon;

    public IEnumerator GetCoordinates(string address)
    {
        string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={UnityWebRequest.EscapeURL(address)}&key={API_KEY}";
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            var locationData = JsonUtility.FromJson<GeocodingResponse>(json);

            if (locationData.status == "OK")
            {
                destinationLat = locationData.results[0].geometry.location.lat;
                destinationLon = locationData.results[0].geometry.location.lng;
                Debug.Log($"Destination Coordinates: {destinationLat}, {destinationLon}");
            }
            else
            {
                Debug.LogError($"Geocoding failed: {locationData.status}");
            }
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }
}

[System.Serializable]
public class GeocodingResponse
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
