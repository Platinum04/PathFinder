using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARNavigationManager : MonoBehaviour
{
    public GameObject arPrefab; // Prefab for AR content
    public LocationFetcher locationFetcher;
    public Text statusTxt, locationTxt;

    private const float EarthRadius = 6371000; // Radius of Earth in meters
    private GameObject arrowInstance; // Track the arrow instance
    private Vector3 destinationUnityPosition; // Destination in Unity world space

    public static Vector3 GPSPointToUnityVector(float originLat, float originLon, float targetLat, float targetLon)
    {
        float dLat = Mathf.Deg2Rad * (targetLat - originLat);
        float dLon = Mathf.Deg2Rad * (targetLon - originLon);
        float lat1 = Mathf.Deg2Rad * originLat;

        float x = EarthRadius * dLon * Mathf.Cos(lat1);
        float z = EarthRadius * dLat;

        return new Vector3(x, 0, z); // Map to Unity's X-Z plane
    }

    public void StartARNavigation()
    {
        StartCoroutine(InitializeDestination());
    }

    IEnumerator InitializeDestination()
    {
        // Wait for GPSManager to initialize GPS
        yield return new WaitUntil(() => locationFetcher.originLat != 0 && locationFetcher.originLng != 0);

        // Calculate the Unity position of the destination
        destinationUnityPosition = GPSPointToUnityVector(
            locationFetcher.originLat,
            locationFetcher.originLng,
            locationFetcher.destLat,
            locationFetcher.destLng
        );

        locationTxt.text = locationFetcher.originLat.ToString() + ", " + locationFetcher.originLng.ToString() + ", " +
                           locationFetcher.destLat.ToString() + ", " + locationFetcher.destLng.ToString();

        // Instantiate the arrow
        arrowInstance = Instantiate(arPrefab, Vector3.zero, Quaternion.identity);

        // Start updating the arrow's position and orientation in real-time
        StartCoroutine(UpdateArrowPosition());
    }

    IEnumerator UpdateArrowPosition()
    {
        while (true)
        {
            // Fetch current location
            Vector3 currentUnityPosition = GPSPointToUnityVector(
                locationFetcher.originLat,
                locationFetcher.originLng,
                locationFetcher.originLat, // Use updated origin GPS here
                locationFetcher.originLng
            );

            // Calculate direction to destination
            Vector3 directionToDestination = (destinationUnityPosition - currentUnityPosition).normalized;

            // Update arrow's position (optional: offset to be in front of the camera)
            arrowInstance.transform.position = currentUnityPosition + directionToDestination * 0.5f; // Offset for visibility

            // Update arrow's rotation to point towards the destination
            arrowInstance.transform.rotation = Quaternion.LookRotation(directionToDestination);

            // Update status text for debugging
            statusTxt.text = $"Arrow Updated: {currentUnityPosition} -> {destinationUnityPosition}";

            yield return new WaitForSeconds(1f); // Update every second
        }
    }
}
