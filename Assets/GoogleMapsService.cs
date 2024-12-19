using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// Define classes for the Google Distance Matrix API response
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
    public string status; // Renamed to 'elementStatus' if conflicts occur
    public Distance distance;
}

[System.Serializable]
public class Distance
{
    public string text;
    public int value;
}

public class GoogleMapsService : MonoBehaviour
{
    private const string API_KEY = "AIzaSyBeuY4Zwi0eslU4NBBcHIovxrx4cWIcib0";  // Use a secure method to fetch this in production
    public Text DistanceDisplay;  // UI Text for displaying the distance

    /// <summary>
    /// Fetches the distance between two locations using Google Maps Distance Matrix API.
    /// </summary>
    public IEnumerator GetDistance(float originLat, float originLng, float destLat, float destLng)
    {
        // Construct the API request URL
        string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={originLat},{originLng}&destinations={destLat},{destLng}&key={API_KEY}";
        UnityWebRequest request = UnityWebRequest.Get(url);

        // Send the API request
        yield return request.SendWebRequest();
    }

    private static string GetCoordinates1(string address, Action<float, float> onCoordinatesReceived)
    {
        throw new NotImplementedException();
    }

    internal string GetCoordinates(string address, Action<float, float> onCoordinatesReceived)
    {
        throw new NotImplementedException();
    }
}