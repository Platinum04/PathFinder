// Data Models for Google Geocoding API Response
[System.Serializable]
public class GeocodingResponse
{
    public string status;   // API request status (e.g., "OK")
    public Result[] results;  // List of location results
}

[System.Serializable]
public class Result
{
    public AddressComponent[] address_components;  // Address details
    public Geometry geometry;  // Lat/Lng and bounds
    public string formatted_address;  // Full address as a readable string
}

[System.Serializable]
public class AddressComponent
{
    public string long_name;    // Full name of the location
    public string short_name;   // Abbreviated name
    public string[] types;      // Types of address components
}

[System.Serializable]
public class Geometry
{
    public Location location;  // Lat/Lng coordinates
    public Bounds bounds;  // Location bounds
    public Viewport viewport;  // Recommended viewport for map display
}

[System.Serializable]
public class Bounds
{
    public Location northeast;  // Northeast corner of bounds
    public Location southwest;  // Southwest corner of bounds
}

[System.Serializable]
public class Viewport
{
    public Location northeast;  // Northeast corner of viewport
    public Location southwest;  // Southwest corner of viewport
}

[System.Serializable]
public class Location
{
    public float lat;  // Latitude
    public float lng;  // Longitude
}
