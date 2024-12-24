using UnityEngine;

public class ARNavigationManager : MonoBehaviour
{
    public GameObject arrowPrefab;  // Assign the Arrow Prefab
    public Transform arCamera;      // Assign AR Camera in Inspector
    private GameObject arrowInstance;

    public void StartNavigation(float userLat, float userLng, float destLat, float destLng)
    {
        // Convert destination GPS to Unity world space
        Vector3 destinationPosition = GPSLocationToWorld.ConvertToUnityWorldSpace(userLat, userLng, destLat, destLng);

        // Instantiate the arrow if it doesn't exist
        if (arrowInstance == null)
        {
            arrowInstance = Instantiate(arrowPrefab, arCamera.position, Quaternion.identity);
            arrowInstance.transform.SetParent(arCamera);  // Attach to AR Camera
        }

        // Set the destination in ARNavigationArrow
        arrowInstance.GetComponent<ARNavigationArrow>().SetDestination(destinationPosition);
    }
}
