using AirportWeather.Models;

namespace AirportWeather.Data;

public static class AirportData
{
    public static readonly List<Airport> All = new()
    {
        new("SFO", "San Francisco",   37.6213, -122.3790),
        new("LAX", "Los Angeles",     33.9416, -118.4085),
        new("JFK", "New York JFK",    40.6413,  -73.7781),
        new("ORD", "Chicago O'Hare",  41.9742,  -87.9073),
        new("SEA", "Seattle-Tacoma",  47.4502, -122.3088),
        new("LHR", "London Heathrow", 51.4700,   -0.4543),
        new("NRT", "Tokyo Narita",    35.7720,  140.3929),
        new("DXB", "Dubai",           25.2532,   55.3657),
    };
}