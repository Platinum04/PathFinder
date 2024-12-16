using UnityEngine;

public class ARNavigation : MonoBehaviour
{
    public GameObject arrowPrefab;
    private GameObject arrowInstance;
    private Vector3 destination;

    void Start()
    {
        arrowInstance = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity);
        arrowInstance.transform.SetParent(Camera.main.transform, false);
        arrowInstance.transform.localPosition = new Vector3(0, -0.5f, 2);  // In front of the camera
    }

    void Update()
    {
        if (destination != Vector3.zero)
        {
            Vector3 userPosition = Camera.main.transform.position;
            Vector3 targetPosition = new Vector3(destination.x, userPosition.y, destination.z);
            Vector3 directionToDestination = targetPosition - userPosition;

            arrowInstance.transform.rotation = Quaternion.LookRotation(directionToDestination);
        }
    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
    }
}
