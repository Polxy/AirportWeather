namespace AirportWeather.Models;

public class WeatherInfo
{
    public double Temperature {get; set;}
    public double Humidity {get; set;}
    public double WindSpeed {get; set;}
    public string Condition {get; set;} = "";
    public DateTime LastUpdated {get; set;}
}