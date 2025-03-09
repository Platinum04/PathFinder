using UnityEngine;

public class ARNavigation : MonoBehaviour
{
    public GameObject arrowPrefab;   // Prefab for the navigation arrow
    private GameObject arrowInstance; // The single active arrow
    private Vector3 destination;
    private Vector3 lastArrowPosition; // Track last arrow's position
    private float spawnDistance = 0.9f; // 3 feet (~0.9 meters)

    void Start()
    {
        if (arrowInstance == null)
        {
            arrowInstance = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity);
            arrowInstance.transform.SetParent(Camera.main.transform, false);
            arrowInstance.transform.localPosition = new Vector3(0, -0.5f, 2);  // Start in front of the camera
            lastArrowPosition = arrowInstance.transform.position; // Set initial position
        }
    }

    void Update()
    {
        if (destination != Vector3.zero)
        {
            Vector3 userPosition = Camera.main.transform.position;
            Vector3 targetPosition = new Vector3(destination.x, userPosition.y, destination.z);
            Vector3 directionToDestination = targetPosition - userPosition;

            // Move arrow instead of spawning multiple
            float distanceFromLastArrow = Vector3.Distance(lastArrowPosition, userPosition);

            if (distanceFromLastArrow >= spawnDistance)  // Check if user moved 3 feet
            {
                lastArrowPosition = userPosition; // Update last arrow position
                MoveArrow(userPosition, directionToDestination);
            }
        }
    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
    }

    private void MoveArrow(Vector3 position, Vector3 direction)
    {
        arrowInstance.transform.position = position + direction.normalized * 1.5f; // Slightly ahead
        arrowInstance.transform.rotation = Quaternion.LookRotation(direction);
    }
}
