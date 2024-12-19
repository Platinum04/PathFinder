using UnityEngine;
using UnityEngine.UI;

public class NavigationUI : MonoBehaviour
{
    public InputField destinationInput;
    public Button startNavButton;
    public Text statusText;

    public LocationFetcher locationFetcher;
    public ARNavigation aRNavigation;

    void Start()
    {
        // Find LocationFetcher and register button listener
        locationFetcher = FindObjectOfType<LocationFetcher>();
        startNavButton.onClick.AddListener(OnNavigateClick);
    }

    // Method Triggered by Button
    public void OnNavigateClick()
    {
        Debug.Log("Button Clicked: Start Navigation Triggered");  // Debug Statement

        if (locationFetcher != null)
        {
            locationFetcher.OnSearchButtonClicked();  // Trigger Location Search
        }
        else
        {
            Debug.LogError("Error: LocationFetcher component is missing!");
        }
    }
}
