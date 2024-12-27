using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // For transitioning to AR Scene

public class LocationFetcher : MonoBehaviour
{
    public InputField destinationInput;          // User input for destination
    public Text statusText, UserLocation, destinationTxt, currentLocationText;  // UI status and location displays
    public GoogleMapsService googleMapsService; // Reference to Google Maps Service
    public float originLat, originLng, destLat, destLng;  // Coordinates for origin and destination
    public GameObject resultScreen, homeScreen;

    private void Start()
    {
        // Subscribe to GoogleMapsService events
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
            destLat = lat;  // Set destination latitude
            destLng = lng;  // Set destination longitude

            Debug.Log($"Destination Coordinates: Latitude {lat}, Longitude {lng}");
            homeScreen.SetActive(false);
            resultScreen.SetActive(true);
        }
        else
        {
            statusText.text = message;
            Debug.LogError("Error: Invalid destination coordinates received.");
        }
    }

    // Handle the user's current coordinates received via GPS
    private void OnUserCoordinatesReceived(float lat, float lng, string status)
    {
        if (lat != 0 && lng != 0)
        {
            statusText.text = status;
            originLat = lat;  // Set user's current latitude
            originLng = lng;  // Set user's current longitude

            Debug.Log($"User Coordinates: Latitude {lat}, Longitude {lng}");
        }
        else
        {
            statusText.text = status;
            Debug.LogError("Error: Invalid user coordinates received.");
        }
    }

    // Display the origin and destination locations in words
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
