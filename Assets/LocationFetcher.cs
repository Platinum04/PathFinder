using UnityEngine;
using UnityEngine.UI;

public class LocationFetcher : MonoBehaviour
{
    public InputField destinationInput;
    public Text statusText;
    private GoogleMapsService googleMapsService;

    void Start()
    {
        googleMapsService = FindObjectOfType<GoogleMapsService>();
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

    private void OnCoordinatesReceived(float lat, float lng)
    {
        statusText.text = $"Coordinates Found: {lat}, {lng}";
        Vector3 destination = new Vector3(lat, 0, lng);
        FindObjectOfType<ARNavigation>().SetDestination(destination);
    }
}
