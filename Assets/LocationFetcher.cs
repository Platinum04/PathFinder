using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationFetcher : MonoBehaviour
{
    public InputField destinationInput;   // User destination input field
    public Text statusText, UserLocation, destinationTxt, currentLocationText;              // Status messages display
    public GoogleMapsService googleMapsService;  // Reference to Google Maps Service
    public float originLat, originLng, destLat, destLng;

    private void Start()
    {
        googleMapsService.locationData += DisplayLocationInWords;
        googleMapsService.userLocationReceived += OnUserCoordinatesReceived;
    }

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
        }
        else
        {
            statusText.text = "Service unavailable.";
            Debug.LogError("Google Maps Service is not assigned!");
        }
    }

    // Handle the coordinates received from Google Maps
    private void OnCoordinatesReceived(float lat, float lng, string message)
    {
        if (lat != 0 && lng != 0)
        {
            statusText.text = message;
            destLat = lat;
            destLng = lng;
        }
        else
        {
            statusText.text = message;
            Debug.LogError("Error: Invalid coordinates received.");
        }
    }
    // Handle the coordinates received from Google Maps
    private void OnUserCoordinatesReceived(float lat, float lng, string status)
    {
        if (lat != 0 && lng != 0)
        {
            statusText.text = status;
            originLat = lat;
            originLng = lng;
            Debug.Log($"Coordinates Found: Latitude {lat}, Longitude {lng}");
        }
        else
        {
            statusText.text = status;
            Debug.LogError("Error: Invalid coordinates received.");
        }
    }

    private void DisplayLocationInWords(string originAddress, string destinationAddress, string message)
    {
        if (!string.IsNullOrEmpty(originAddress))
        {
            currentLocationText.text = $"Your Location: {originAddress}";
            destinationTxt.text = $"Your Destination: {destinationAddress}";
            Debug.Log($"User Current Address: {originAddress}");
            Debug.Log($"User Destination Address: {destinationAddress}");
        }
        else
        {
            statusText.text = message;
        }
    }

}
