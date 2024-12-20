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
    public string formatted_address;  // Optional but useful
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
