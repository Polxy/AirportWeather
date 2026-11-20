namespace AirportWeather.Models;

public class WatchedAirport
{
    public required Airport Airport { get; init; }
    public WeatherInfo? Weather { get; set; }
    public string? Error { get; set; }
    public bool IsLoading { get; set; }

    // How much the temperature changed in the last refresh (null = no notable change)
    public double? LastChange { get; set; }
}