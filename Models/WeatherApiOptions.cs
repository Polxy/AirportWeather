namespace AirportWeather.Models;

public class WeatherApiOptions
{
    public const string SectionName = "WeatherApi";

    public string BaseUrl { get; set; } = "";
    public string ApiKey { get; set; } = "";
}