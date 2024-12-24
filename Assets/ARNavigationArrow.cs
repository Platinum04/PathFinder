using UnityEngine;

public class ARNavigationArrow : MonoBehaviour
{
    public Transform arCamera;  // Assign AR Camera in Inspector
    private Vector3 destinationPosition;

    public void SetDestination(Vector3 destination)
    {
        destinationPosition = destination;
    }

    void Update()
    {
        if (destinationPosition != Vector3.zero)
        {
            // Calculate direction to the destination
            Vector3 direction = destinationPosition - arCamera.position;
            direction.y = 0;  // Keep the arrow horizontal

            // Rotate the arrow to point toward the destination
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
