using AirportWeather.Models;

namespace AirportWeather.Services;

public interface IWeatherService
{
    Task<WeatherInfo> GetCurrentWeatherAsync(Airport airport);
}