using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARNavigation : MonoBehaviour
{
    public GameObject arrowPrefab;
    private GameObject arrowInstance;
    private Vector2 destination;

    void Start()
    {
        arrowInstance = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity);
        arrowInstance.transform.SetParent(Camera.main.transform);
    }

    void Update()
    {
        if (destination != Vector2.zero)
        {
            Vector3 userPosition = transform.position;
            Vector3 destinationPosition = new Vector3(destination.x, userPosition.y, destination.y); // Assuming flat plane for simplicity
            Vector3 directionToDestination = destinationPosition - userPosition;

            // Rotate arrow to point towards destination
            arrowInstance.transform.rotation = Quaternion.LookRotation(directionToDestination);
        }
    }

    public void SetDestination(Vector2 dest)
    {
        destination = dest;
    }
}