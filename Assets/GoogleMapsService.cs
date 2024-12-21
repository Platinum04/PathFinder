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
    public string status; // Overall API status
    public string[] destination_addresses; // Destination addresses array
    public string[] origin_addresses; // Origin addresses array
    public Row[] rows; // Rows containing distance and duration information
}

[System.Serializable]
public class Row
{
    public Element[] elements;
}

[System.Serializable]
public class Element
{
    public string status; // Element-specific status
    public Distance distance; // Distance information
    public Duration duration; // Duration information
}

[System.Serializable]
public class Distance
{
    public string text;  // Human-readable distance (e.g., "5.1 km")
    public int value;    // Distance in meters
}

[System.Serializable]
public class Duration
{
    public string text; // Human-readable duration (e.g., "12 mins")
    public int value; // Duration in seconds
}

public class GoogleMapsService : MonoBehaviour
{
    private const string API_KEY = "AIzaSyBeuY4Zwi0eslU4NBBcHIovxrx4cWIcib0";  // Replace with your actual API key
    public Text drivingDataTxt, walkingDataTxt;  // UI Text for displaying the distance
    public LocationFetcher locationFetcher;
    public Action<string, string, string> locationData;
    public Action<float, float, string> userLocationReceived;

    
    /// <summary>
    /// Fetches the coordinates for a given address using Google Maps Geocoding API.
    /// </summary>
    public IEnumerator GetCoordinates(string address, Action<float, float, string> onCoordinatesReceived)
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
                    float destinationLat = locationData.results[0].geometry.location.lat;
                    float destinationLng = locationData.results[0].geometry.location.lng;

                    Debug.Log($"Coordinates Found: Lat {destinationLat}, Lng {destinationLng}");
                    onCoordinatesReceived?.Invoke(destinationLat, destinationLng, "Location Found");

                    // Ensure locationFetcher and origin coordinates are valid
                    if (locationFetcher != null)
                    {
                        //For Production
                        StartCoroutine(GetUserLocation(destinationLat, destinationLng));

                        //For testing
                        //StartCoroutine(GetDistance(locationFetcher.originLat, locationFetcher.originLng, destinationLat, destinationLng, "driving", drivingDataTxt));
                        //StartCoroutine(GetDistance(locationFetcher.originLat, locationFetcher.originLng, destinationLat, destinationLng, "walking", walkingDataTxt));
                    }
                    else
                    {
                        Debug.LogError("Error: originLat and originLng are not set or locationFetcher is null.");
                    }
                }
                else
                {
                    onCoordinatesReceived?.Invoke(0, 0, "Can't find location");
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


    public IEnumerator GetUserLocation(float destinationLat, float destinationLng)
    {
        // Check if the user has granted permission for location services
        if (!Input.location.isEnabledByUser)
        {
            userLocationReceived?.Invoke(0f, 0f, "Location not enabled"); // Return 0,0 or any default values
            Debug.LogError("Location Services not enabled by user");
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
            userLocationReceived?.Invoke(0f, 0f, "Timed out");
            Debug.Log("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            userLocationReceived?.Invoke(0f, 0f, "Unable to determine device location");
            Debug.LogError("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;

            userLocationReceived?.Invoke(latitude, longitude, "Service status: Active");
            Debug.Log($"User Location: Lat {latitude}, Lng {longitude}");

            StartCoroutine(GetDistance(locationFetcher.originLat, locationFetcher.originLng, destinationLat, destinationLng, "driving", drivingDataTxt));
            StartCoroutine(GetDistance(locationFetcher.originLat, locationFetcher.originLng, destinationLat, destinationLng, "walking", walkingDataTxt));
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }


    public IEnumerator GetDistance(float originLat, float originLng, float destLat, float destLng, string travelMode, Text distanceTxt)
    {
        string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={originLat},{originLng}&destinations={destLat},{destLng}&mode={travelMode}&key={API_KEY}";
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            Debug.Log($"Full API Response ({travelMode}): {json}");

            try
            {
                DistanceMatrixResponse distanceData = JsonConvert.DeserializeObject<DistanceMatrixResponse>(json);

                if (distanceData != null && distanceData.status == "OK")
                {
                    string originAddress = distanceData.origin_addresses.Length > 0 ? distanceData.origin_addresses[0] : null;
                    string destinationAddress = distanceData.destination_addresses.Length > 0 ? distanceData.destination_addresses[0] : null;

                    if (distanceData.rows.Length > 0 && distanceData.rows[0].elements.Length > 0)
                    {
                        Element element = distanceData.rows[0].elements[0];
                        if (element.status == "OK")
                        {
                            string distanceText = element.distance.text;
                            string durationText = element.duration.text;

                            Debug.Log($"{travelMode} - Distance: {distanceText}, Duration: {durationText}");

                            // Update the UI or trigger a callback
                            distanceTxt.text = $"{travelMode.ToUpper()}: Distance: {distanceText}, Duration: {durationText}\n";
                            locationData?.Invoke(originAddress, destinationAddress, $"Direction Found ({travelMode})");
                        }
                        else
                        {
                            Debug.LogError($"Element status ({travelMode}): {element.status}");
                        }
                    }
                    else
                    {
                        Debug.LogError($"No distance data available for {travelMode}.");
                    }
                }
                else
                {
                    Debug.LogError($"Distance Matrix API returned an error status for {travelMode}.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON Parsing Error ({travelMode}): {ex.Message}");
            }
        }
        else
        {
            Debug.LogError($"Request Error ({travelMode}): {request.error}");
        }
    }
}
