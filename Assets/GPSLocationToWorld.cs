using UnityEngine;

public class GPSLocationToWorld : MonoBehaviour
{
    public static Vector3 ConvertToUnityWorldSpace(float userLat, float userLng, float destLat, float destLng)
    {
        float scale = 111320f;  // Meters per degree of latitude
        float deltaLat = destLat - userLat;
        float deltaLng = destLng - userLng;

        // Convert lat/lng difference to Unity space
        float x = deltaLng * Mathf.Cos(userLat * Mathf.Deg2Rad) * scale;
        float z = deltaLat * scale;

        return new Vector3(x, 0, z);  // Unity coordinates
    }
}
