using System.Globalization;
using System.Net.Http.Json;
using AirportWeather.Models;

namespace AirportWeather.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _http;

    public WeatherService(HttpClient http)
    {
        _http = http;
    }

    public async Task<WeatherInfo> GetCurrentWeatherAsync(Airport airport)
    {
        var lat = airport.Latitude.ToString(CultureInfo.InvariantCulture);
        var lon = airport.Longitude.ToString(CultureInfo.InvariantCulture);

        var url = $"v1/forecast?latitude={lat}&longitude={lon}" +
                  "&current=temperature_2m,relative_humidity_2m,wind_speed_10m,weather_code";

        var response = await _http.GetFromJsonAsync<OpenMeteoResponse>(url);

        if (response?.Current is null)
            throw new InvalidOperationException("The weather API returned no data.");

        var c = response.Current;

        return new WeatherInfo
        {
            Temperature = c.Temperature,
            Humidity = c.Humidity,
            WindSpeed = c.WindSpeed,
            Condition = ToCondition(c.WeatherCode),
            LastUpdated = DateTime.Now,
        };

    }

    private static string ToCondition(int code) => code switch
    {
        0 => "Clear",
        >= 1 and <= 3 => "Cloudy",
        45 or 48 => "Fog",
        >= 51 and <= 67 => "Rain",
        >= 71 and <= 77 => "Snow",
        >= 95 => "Thunderstorm",
        _ => "Other",
    };
}