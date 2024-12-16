using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleMapsService : MonoBehaviour
{
    private const string API_KEY = "AIzaSyBeuY4Zwi0eslU4NBBcHIovxrx4cWIcib0";  // Replace with your API key

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
}
