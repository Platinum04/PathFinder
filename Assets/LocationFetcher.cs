using UnityEngine;
using UnityEngine.UI;
using System.Collections;  // Required for IEnumerator
using System.Collections.Generic;

public class LocationFetcher : MonoBehaviour
{
    [Header("UI Elements")]
    public InputField destinationInput;
    public Text statusText;
    public Text distanceText;  // For displaying the distance
    public GoogleMapsService googleMapsService;
    public ARNavigation arNavigation;

    private float userLatitude;
    private float userLongitude;

    private void Start()
    {
        // Initialize components
        googleMapsService = FindObjectOfType<GoogleMapsService>();
        arNavigation = FindObjectOfType<ARNavigation>();

        // Start getting the user's current location
        StartCoroutine(GetUserLocation());
    }

    public void OnSearchButtonClicked()
    {
        string address = destinationInput.text.Trim();

        if (!string.IsNullOrEmpty(address))
        {
            statusText.text = "Fetching location...";
            StartCoroutine(googleMapsService.GetCoordinates(address, OnCoordinatesReceived));
        }
        else
        {
            statusText.text = "Please enter a valid destination.";
        }
    }

    private void OnCoordinatesReceived(float destLat, float destLng)
    {
        // Request distance calculation
        StartCoroutine(googleMapsService.GetDistance(userLatitude, userLongitude, destLat, destLng, OnDistanceReceived));

        // Proceed to AR navigation
        Vector3 destination = new Vector3(destLat, 0, destLng);
        arNavigation.SetDestination(destination);
    }

    private void OnDistanceReceived(string distance)
    {
        distanceText.text = $"Distance: {distance}";
        statusText.text = "Distance calculated. Starting AR navigation!";
    }

    private IEnumerator GetUserLocation()
    {
        if (!Input.location.isEnabledByUser)
        {
            statusText.text = "Location services are disabled.";
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            statusText.text = "Failed to get GPS location.";
        }
        else
        {
            userLatitude = Input.location.lastData.latitude;
            userLongitude = Input.location.lastData.longitude;
            statusText.text = "Location obtained.";
        }

        Input.location.Stop();
    }
}
