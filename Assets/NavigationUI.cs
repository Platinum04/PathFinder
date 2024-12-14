using UnityEngine;
using UnityEngine.UI;

public class NavigationUI : MonoBehaviour
{
    public InputField destinationInput;
    public Button startNavButton;
    public Text statusText;

    void Start()
    {
        startNavButton.onClick.AddListener(OnNavigateClick);
    }

    void OnNavigateClick()
    {
        string address = destinationInput.text;
        // Call method to fetch coordinates and start navigation
        FindObjectOfType<LocationFetcher>().addressToGeocode = address;
        FindObjectOfType<LocationFetcher>().FetchCoordinates();
        statusText.text = "Fetching destination...";
    }
}