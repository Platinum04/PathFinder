using UnityEngine;
using UnityEngine.UI;

public class NavigationUI : MonoBehaviour
{
    // UI Elements to assign in Unity
    public InputField destinationInput;
    public Button startNavButton;
    public Text statusText;

    private LocationFetcher locationFetcher;

    // Initialize the UI setup
    void Start()
    {
        // Find the LocationFetcher script
        locationFetcher = FindObjectOfType<LocationFetcher>();

        // Add listener to the Start Navigation button
        startNavButton.onClick.AddListener(OnNavigateClick);
    }

    // Called when the Start Navigation button is clicked
    public void OnNavigateClick()
    {
        if (locationFetcher != null)
        {
            locationFetcher.OnSearchButtonClicked();  // Trigger location fetching
        }
        else
        {
            statusText.text = "LocationFetcher not found.";
            Debug.LogError("LocationFetcher component is missing!");
        }
    }
}
