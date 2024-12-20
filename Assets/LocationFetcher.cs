using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationFetcher : MonoBehaviour
{
    public InputField destinationInput;   // User destination input field
    public Text statusText, UserLocation;              // Status messages display
    public GoogleMapsService googleMapsService;  // Reference to Google Maps Service

    // Called when the Search Button is clicked
    public void OnSearchButtonClicked()
    {
        // Validate the input
        string address = destinationInput.text.Trim();

        if (string.IsNullOrEmpty(address))
        {
            statusText.text = "Please enter a valid destination.";
            return;
        }

        // Display loading message
        statusText.text = "Fetching location...";

        // Start Google Maps API request
        if (googleMapsService != null)
        {
            StartCoroutine(googleMapsService.GetCoordinates(address, OnCoordinatesReceived));
            StartCoroutine(googleMapsService.GetUserLocation(OnUserCoordinatesReceived));
        }
        else
        {
            statusText.text = "Service unavailable.";
            Debug.LogError("Google Maps Service is not assigned!");
        }
    }

    // Handle the coordinates received from Google Maps
    private void OnCoordinatesReceived(float lat, float lng)
    {
        if (lat != 0 && lng != 0)
        {
            statusText.text = $"Coordinates Found: {lat}, {lng}";
            Debug.Log($"Coordinates Found: Latitude {lat}, Longitude {lng}");
        }
        else
        {
            statusText.text = "Failed to fetch coordinates.";
            Debug.LogError("Error: Invalid coordinates received.");
        }
    }
    // Handle the coordinates received from Google Maps
    private void OnUserCoordinatesReceived(float lat, float lng)
    {
        if (lat != 0 && lng != 0)
        {
            UserLocation.text = $"Coordinates Found: {lat}, {lng}";
            Debug.Log($"Coordinates Found: Latitude {lat}, Longitude {lng}");
        }
        else
        {
            UserLocation.text = "Failed to fetch coordinates.";
            Debug.LogError("Error: Invalid coordinates received.");
        }
    }
}
