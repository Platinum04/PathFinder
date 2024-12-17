using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleMapsService : MonoBehaviour
{
    private const string API_KEY = "AIzaSyBeuY4Zwi0eslU4NBBcHIovxrx4cWIcib0";  // Replace with your actual API key

    /// <summary>
    /// Fetches the distance between the user's location and destination using Google Maps Distance Matrix API.
    /// </summary>
    public IEnumerator GetDistance(float originLat, float originLng, float destLat, float destLng, System.Action<string> callback)
    {
        // Construct the API request URL
        string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={originLat},{originLng}&destinations={destLat},{destLng}&key={API_KEY}";

        // Send the API request
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        // Handle request success
        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;

            // Debug: Log the full response
            Debug.Log($"Full Distance Matrix Response: {json}");

            // Deserialize the API response
            DistanceMatrixResponse distanceData = JsonUtility.FromJson<DistanceMatrixResponse>(json);

            // Check response validity
            if (distanceData.status == "OK" &&
                distanceData.rows != null &&
                distanceData.rows.Length > 0 &&
                distanceData.rows[0].elements.Length > 0 &&
                distanceData.rows[0].elements[0].status == "OK")
            {
                // Extract and pass the distance text
                string distanceText = distanceData.rows[0].elements[0].distance.text;
                Debug.Log($"Distance Calculation Successful: {distanceText}");
                callback?.Invoke(distanceText);  // Pass the result to the callback
            }
            else
            {
                Debug.LogError("Distance calculation failed: Invalid API response.");
            }
        }
        else
        {
            Debug.LogError($"Distance Request Error: {request.error}");
        }
    }
}
