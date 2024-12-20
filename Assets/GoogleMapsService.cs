using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;

// Data models for the Google Distance Matrix API response
[System.Serializable]
public class DistanceMatrixResponse
{
    public string status;
    public Row[] rows;
}

[System.Serializable]
public class Row
{
    public Element[] elements;
}

[System.Serializable]
public class Element
{
    public string status;  // Element request status
    public Distance distance;
}

[System.Serializable]
public class Distance
{
    public string text;  // Human-readable distance (e.g., "5.1 km")
    public int value;    // Distance in meters
}

public class GoogleMapsService : MonoBehaviour
{
    private const string API_KEY = "AIzaSyBeuY4Zwi0eslU4NBBcHIovxrx4cWIcib0";  // Replace with your actual API key
    public Text DistanceDisplay;  // UI Text for displaying the distance

    /// <summary>
    /// Fetches the distance between two locations using Google Maps Distance Matrix API.
    /// </summary>
    public IEnumerator GetDistance(float originLat, float originLng, float destLat, float destLng)
    {
        string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={originLat},{originLng}&destinations={destLat},{destLng}&key={API_KEY}";
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            Debug.Log($"Full API Response: {json}");

            try
            {
                DistanceMatrixResponse distanceData = JsonConvert.DeserializeObject<DistanceMatrixResponse>(json);

                if (distanceData != null && distanceData.status == "OK" && distanceData.rows.Length > 0 && distanceData.rows[0].elements.Length > 0)
                {
                    string distanceText = distanceData.rows[0].elements[0].distance.text;
                    int distanceMeters = distanceData.rows[0].elements[0].distance.value;

                    DistanceDisplay.text = $"Distance: {distanceText} ({distanceMeters} meters)";
                    Debug.Log($"Distance: {distanceText} | {distanceMeters} meters");
                }
                else
                {
                    DistanceDisplay.text = "Distance not found.";
                    Debug.LogError("Distance calculation failed.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON Parsing Error: {ex.Message}");
            }
        }
        else
        {
            DistanceDisplay.text = "Error: Check your internet connection.";
            Debug.LogError($"Request Error: {request.error}");
        }
    }

    /// <summary>
    /// Fetches the coordinates for a given address using Google Maps Geocoding API.
    /// </summary>
    public IEnumerator GetCoordinates(string address, Action<float, float> onCoordinatesReceived)
    {
        string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={UnityWebRequest.EscapeURL(address)}&key={API_KEY}";
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            Debug.Log($"Geocoding API Response: {json}");

            try
            {
                GeocodingResponse locationData = JsonConvert.DeserializeObject<GeocodingResponse>(json);

                if (locationData != null && locationData.status == "OK" && locationData.results.Length > 0)
                {
                    float lat = locationData.results[0].geometry.location.lat;
                    float lng = locationData.results[0].geometry.location.lng;

                    Debug.Log($"Coordinates Found: Lat {lat}, Lng {lng}");
                    onCoordinatesReceived?.Invoke(lat, lng);
                   

                }
                else
                {
                    Debug.LogError("Geocoding failed: Invalid API response.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON Parsing Error: {ex.Message}");
            }
        }
        else
        {
            Debug.LogError($"Request Error: {request.error}");
        }
    }
    public IEnumerator GetUserLocation(Action<float, float> onLocationReceived)
    {
        // Check if the user has granted permission for location services
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("Location Services not enabled by user");
            onLocationReceived?.Invoke(0f, 0f); // Return 0,0 or any default values
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            onLocationReceived?.Invoke(0f, 0f);
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            onLocationReceived?.Invoke(0f, 0f);
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;

            Debug.Log($"User Location: Lat {latitude}, Lng {longitude}");
            onLocationReceived?.Invoke(latitude, longitude);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}
