using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GoogleMapsService : MonoBehaviour
{
    private const string API_KEY = "AIzaSyBeuY4Zwi0eslU4NBBcHIovxrx4cWIcib0";  // Replace with your actual API key

    /// <summary>
    /// Fetches GPS coordinates from Google Geocoding API.
    /// </summary>
    public IEnumerator GetCoordinates(string address, System.Action<float, float> callback)
    {
        string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={UnityWebRequest.EscapeURL(address)}&key={API_KEY}";
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            GeocodingResponse locationData = JsonUtility.FromJson<GeocodingResponse>(json);

            if (locationData.status == "OK")
            {
                float lat = locationData.results[0].geometry.location.lat;
                float lng = locationData.results[0].geometry.location.lng;
                callback?.Invoke(lat, lng);
            }
            else
            {
                Debug.LogError($"Geocoding failed: {locationData.status}");
            }
        }
        else
        {
            Debug.LogError($"Request Error: {request.error}");
        }
    }

    /// <summary>
    /// Fetches the distance between two locations using Google Maps Distance Matrix API.
    /// </summary>
    public IEnumerator GetDistance(float originLat, float originLng, float destLat, float destLng, System.Action<string> callback)
    {
        string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={originLat},{originLng}&destinations={destLat},{destLng}&key={API_KEY}";
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            DistanceMatrixResponse distanceData = JsonUtility.FromJson<DistanceMatrixResponse>(json);

            if (distanceData.status == "OK" && distanceData.rows.Length > 0 && distanceData.rows[0].elements.Length > 0)
            {
                string distanceText = distanceData.rows[0].elements[0].distance.text;
                callback?.Invoke(distanceText);
            }
            else
            {
                Debug.LogError("Distance calculation failed.");
            }
        }
        else
        {
            Debug.LogError($"Distance Request Error: {request.error}");
        }
    }
}
