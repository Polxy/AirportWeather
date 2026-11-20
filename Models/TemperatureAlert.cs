namespace AirportWeather.Models;

public record TemperatureAlert(
    string AirportCode,
    double OldTemperature,
    double NewTemperature,
    DateTime Time
)
{
    public double Difference => NewTemperature - OldTemperature;
}
