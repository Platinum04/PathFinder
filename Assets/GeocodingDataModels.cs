// Data models for Google Maps API response
[System.Serializable]
public class GeocodingResponse
{
    public string status;
    public Result[] results;
}

[System.Serializable]
public class Result
{
    public Geometry geometry;
}

[System.Serializable]
public class Geometry
{
    public Location location;
}

[System.Serializable]
public class Location
{
    public float lat;
    public float lng;
}
