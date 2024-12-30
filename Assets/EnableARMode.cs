using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Events; // Ensure this is included for UnityEvent
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils; // For XROrigin

public class EnableARMode : MonoBehaviour
{
    public ARSession session; // Assign in Inspector
    public XROrigin sessionOrigin; // Updated to XROrigin
    public GameObject objectToPlace; // Assign in Inspector
    public GameObject[] objectsToDisable; // Assign in Inspector
    public UnityEvent onARModeEnabled; // Can be used to trigger other events

    private void Start()
    {
        // Start the AR session
        if (session != null)
        {
            session.enabled = true;
        }
        else
        {
            Debug.LogError("AR Session is not assigned in the Inspector.");
        }

        // Subscribe to the session's state changed event correctly
        ARSession.stateChanged += OnSessionStateChanged;
    }

    private void OnSessionStateChanged(ARSessionStateChangedEventArgs args) // Updated method signature
    {
        if (args.state == ARSessionState.Ready)
        {
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }

            onARModeEnabled.Invoke();
            // Optionally, add a tap gesture to place the object here if needed.
        }
    }

    private void OnDestroy()
    {
        ARSession.stateChanged -= OnSessionStateChanged; // Unsubscribe correctly here.
    }
}
