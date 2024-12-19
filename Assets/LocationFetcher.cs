using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationFetcher : MonoBehaviour
{
    public InputField destinationInput;     // Destination input field
    public Text statusText;                 // Status display
    public GoogleMapsService googleMapsService;  // Reference to Google Maps Service

    private void Start()
    {
        if (googleMapsService == null)
        {
            googleMapsService = FindObjectOfType<GoogleMapsService>();
            if (googleMapsService == null)
            {
                Debug.LogError("Google Maps Service is not assigned or missing in the scene!");
                statusText.text = "Google Maps Service unavailable.";
            }
        }

        if (destinationInput == null || statusText == null)
        {
            Debug.LogError("InputField or Text component not assigned in LocationFetcher!");
        }
    }

    // Called when the search button is clicked
    // Assuming this is your current OnSearchButtonClicked method
    public void OnSearchButtonClicked()
    {
        string address = destinationInput.text.Trim();

        if (string.IsNullOrEmpty(address))
        {
            statusText.text = "Please enter a valid destination.";
            return;
        }

        statusText.text = "Fetching location...";

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

    // Called after receiving coordinates
    private void OnCoordinatesReceived(float lat, float lng)
    {
        if (lat != 0 && lng != 0)
        {
            statusText.text = $"Coordinates Found: {lat}, {lng}";
            Debug.Log($"Coordinates Found: Latitude {lat}, Longitude {lng}");
        }
        else
        {
            statusText.text = "Failed to fetch coordinates. Please try again with a different address.";
            Debug.LogError("Error: Invalid coordinates received.");
        }
    }
}