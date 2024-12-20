using UnityEngine;
using UnityEngine.UI;

public class NavigationUI : MonoBehaviour
{
    public LocationFetcher locationFetcher; // Can be private if you set it programmatically

    void Start()
    {
        // If not assigned in the Inspector, try to find it in the scene
        if (locationFetcher == null)
        {
            locationFetcher = FindObjectOfType<LocationFetcher>();
            if (locationFetcher == null)
            {
                Debug.LogError("LocationFetcher not found in the scene!");
            }
        }
    }

    public void OnNavigateClick()
    {
        if (locationFetcher == null)
        {
            Debug.LogError("LocationFetcher reference is missing!");
            return;
        }
        // Use locationFetcher here for navigation or any other functionality
        locationFetcher.OnSearchButtonClicked();
    }
}