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

    public static Vector3 GPSPointToUnityVector(float originLat, float originLon, float targetLat, float targetLon)
    {
        // Calculate differences
        float dLat = Mathf.Deg2Rad * (targetLat - originLat);
        float dLon = Mathf.Deg2Rad * (targetLon - originLon);

        // Convert latitude to radians
        float lat1 = Mathf.Deg2Rad * originLat;

        // Calculate the horizontal distances
        float x = EarthRadius * dLon * Mathf.Cos(lat1);
        float z = EarthRadius * dLat;

        return new Vector3(x, 0, z); // Map to Unity's X-Z plane
    }

    public void StartARNavigation()
    {
        StartCoroutine(PlaceDestinationMarker());
    }

    IEnumerator PlaceDestinationMarker()
    {
        // Wait for GPSManager to initialize GPS
        yield return new WaitUntil(() => locationFetcher.originLat != 0 && locationFetcher.originLng != 0);

        // Convert destination GPS to Unity coordinates
        Vector3 unityPosition = GPSPointToUnityVector(
            locationFetcher.originLat,
            locationFetcher.originLng,
            locationFetcher.destLat,
            locationFetcher.destLng
        );

        locationTxt.text = locationFetcher.originLat.ToString() + ", " + locationFetcher.originLng.ToString() + ", " + locationFetcher.destLat.ToString() + ", " + locationFetcher.destLng.ToString();
        // Place the AR object at the calculated position
        PlaceObjectAtUnityPosition(unityPosition);
    }


    // Method to place an object at a specified Unity world position
    public void PlaceObjectAtUnityPosition(Vector3 unityPosition)
    {
        // Create a new GameObject for the anchor
        GameObject anchorObject = new GameObject("ARAnchorObject");

        // Set the object's position and rotation
        anchorObject.transform.position = unityPosition;
        anchorObject.transform.rotation = Quaternion.identity;

        // Add ARAnchor component to the GameObject
        ARAnchor anchor = anchorObject.AddComponent<ARAnchor>();

        if (anchor != null)
        {
            Vector3 testPosition = new Vector3(0, 0, 1); // 1 meter in front of the camera
            Instantiate(arPrefab, testPosition, Quaternion.identity);

            // Instantiate the AR prefab at the anchor's position
            //Instantiate(arPrefab, anchor.transform.position, anchor.transform.rotation);
            Debug.Log("AR Object placed successfully.");
            statusTxt.text = "AR Object placed successfully.";
        }
        else
        {
            Debug.Log("Failed to create AR Anchor.");
            statusTxt.text = "Failed to create AR Anchor.";
        }
    }
}
