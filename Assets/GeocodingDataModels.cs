// Data models for Geocoding API and Distance Matrix API

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

//////////////////////////////////////////
// Distance Matrix Response Models
//////////////////////////////////////////

[System.Serializable]
public class DistanceMatrixResponse
{
    public string status;
    public Row[] rows;
}

[System.Serializable]
public class Row
{
    public Element[] elements;
}

[System.Serializable]
public class Element
{
    public Distance distance;
    public string status;
}

[System.Serializable]
public class Distance
{
    public string text;  // e.g., "5.1 km"
    public int value;    // Distance in meters
}
