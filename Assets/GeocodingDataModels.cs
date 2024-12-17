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
    public string text;  // Example: "5.1 km"
    public int value;    // Example: 5100 (distance in meters)
}
