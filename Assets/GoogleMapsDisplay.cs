using UnityEngine;

public class GoogleMapsDisplay : MonoBehaviour
{
    // Google Maps Base URL
    private const string GoogleMapsBaseURL = "https://www.google.com/maps/search/?api=1&query=";

    /// <summary>
    /// Opens Google Maps in a web browser at the specified coordinates.
    /// </summary>
    /// <param name="latitude">The latitude of the destination.</param>
    /// <param name="longitude">The longitude of the destination.</param>
    public void ShowGoogleMaps(float latitude, float longitude)
    {
        // Construct the Google Maps URL
        string url = $"{GoogleMapsBaseURL}{latitude},{longitude}";

        // Open the URL in the default web browser
        Application.OpenURL(url);

        Debug.Log($"Opening Google Maps at: {url}");
    }
}
