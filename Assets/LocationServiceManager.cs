using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LocationServiceManager : MonoBehaviour
{
    public float latitude;
    public float longitude;

    public Text x;
    public Text y;
    public Text error1, error2, error3, error4;
    IEnumerator Start()
    {
        // Check if the user has location services enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services not enabled by user.");
            error1.text = "Location services not enabled by user.";
            yield break;
        }

        // Start the location service
        Input.location.Start();

        // Wait until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If location service failed to initialize
        if (maxWait < 1)
        {
            Debug.Log("Location services timed out.");
            error2.text = "Location services timed out.";
            yield break;
        }

        // Location service is ready, so get the coordinates
        if (Input.location.status == LocationServiceStatus.Running)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            Debug.Log("Location: " + latitude + ", " + longitude);
            error3.text = "Location: " + latitude + ", " + longitude;
        }
        else
        {
            Debug.Log("Unable to determine device location.");
            error4.text = "Unable to determine device location.";
        }
    }
}