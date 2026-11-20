# ✈️ Airport Weather Dashboard

A Blazor web app that shows live weather for airports around the world. Add airports to a watchlist, see the current conditions for each one, and get notified when the temperature changes.

Built with **.NET 11**, **Blazor (Interactive Server)** and the free **[Open-Meteo](https://open-meteo.com/)** weather API. No API key needed.

<!-- Add a screenshot: save it as docs/screenshot.png and uncomment the line below -->
<!-- ![Airport Weather Dashboard](docs/screenshot.png) -->

---

## Features

- 🌡️ **Live weather per airport:** temperature, humidity, wind speed and conditions
- ➕ **Add or remove airports** from a dropdown (duplicates are prevented)
- 🔄 **Auto-refresh** every 60 seconds, plus a manual **Refresh now** button
- 🔔 **Temperature change notifications** with a configurable threshold (e.g. notify only if the change is ≥ 0.5 °C)
- 🎨 **Visual highlights:** cards get a red/blue border and a ▲/▼ badge when the temperature rises or falls
- ⚠️ **Per-card error handling:** if one airport fails to load, the others keep working
- 🧪 **Simulate button** (debug) to test notifications without waiting for real weather changes

---

## Tech stack

| Area | Technology |
|---|---|
| Framework | .NET 11, ASP.NET Core |
| UI | Blazor Web App (Interactive Server render mode) |
| Styling | Bootstrap 5 (included in the Blazor template) |
| Weather data | [Open-Meteo Forecast API](https://open-meteo.com/en/docs) |
| HTTP | `HttpClient` via `IHttpClientFactory` (typed client) |
| JSON | `System.Text.Json` |

---

## Getting started

### Prerequisites
- [.NET 11 SDK](https://dotnet.microsoft.com/download) (check with `dotnet --version`)
- An editor: VS Code with the **C# Dev Kit** extension, JetBrains Rider or Visual Studio

### Run locally

```bash
# 1. Clone the repository
git clone https://github.com/<your-username>/AirportWeather.git
cd AirportWeather

# 2. (First time only) trust the local HTTPS development certificate
dotnet dev-certs https --trust

# 3. Run with hot reload
dotnet watch
```

The app opens in your browser automatically. If it doesn't, use the URL shown in the terminal (for example `https://localhost:7xxx`).

---

## Project structure

```
AirportWeather/
├── Program.cs                  # App startup: DI registration + request pipeline
├── Models/
│   ├── Airport.cs              # Airport record (code, name, coordinates)
│   ├── WeatherInfo.cs          # Clean weather model used by the UI
│   ├── OpenMeteoResponse.cs    # DTO matching the Open-Meteo JSON
│   ├── WatchedAirport.cs       # UI state for one card (weather, loading, error, change)
│   └── TemperatureAlert.cs     # A single temperature change notification
├── Data/
│   └── AirportData.cs          # Hardcoded list of available airports
├── Services/
│   ├── IWeatherService.cs      # Service contract
│   └── WeatherService.cs       # Calls Open-Meteo and maps the response
└── Components/
    ├── WeatherCard.razor       # Reusable weather card component
    └── Pages/
        └── Home.razor          # Main dashboard page
```

---

## How it works

1. **`Home.razor`** owns the state: the list of watched airports, the alerts and the threshold.
2. It calls **`IWeatherService`**, which is injected through dependency injection and registered in `Program.cs` as a typed `HttpClient`.
3. **`WeatherService`** requests `api.open-meteo.com`, deserializes the JSON into `OpenMeteoResponse` (the DTO) and maps it to `WeatherInfo`. The `weather_code` value becomes readable text such as "Clear ☀️" or "Rain 🌧️".
4. Each airport is shown in a **`WeatherCard`** component. Data flows down through `[Parameter]` properties, and actions such as Remove or Simulate flow up through `EventCallback`.
5. A **`PeriodicTimer`** refreshes all airports every 60 seconds. After each refresh, the new temperature is compared with the previous one, and an alert is added if the difference is at least the threshold.
6. The timer is stopped through a `CancellationTokenSource` in `Dispose()` when the user leaves the page, so nothing keeps running in the background.

### Example API request

```
https://api.open-meteo.com/v1/forecast?latitude=37.6213&longitude=-122.3790&current=temperature_2m,relative_humidity_2m,wind_speed_10m,weather_code
```

> **Note:** Open-Meteo updates current conditions about every 15 minutes, so real temperature changes won't appear on every 60-second refresh. Use the 🧪 **Simulate** button on a card, then click **Refresh now**, to see a notification.

---

## Adding more airports

Add an entry to `Data/AirportData.cs`:

```csharp
new("CDG", "Paris Charles de Gaulle", 49.0097, 2.5479),
```

---

## Possible improvements

- [ ] Save the selected airports in browser `localStorage` so they survive a page reload
- [ ] °C / °F toggle
- [ ] Unit tests (xUnit) for the change-detection logic, with a fake `IWeatherService`
- [ ] Compare against the temperature at the **last alert** instead of the last reading, so slow, gradual changes also trigger notifications
- [ ] Airport search via an external airport API instead of a hardcoded list
- [ ] Show aviation weather (METAR) from [aviationweather.gov](https://aviationweather.gov/)

---

## Acknowledgements

- Weather data by [Open-Meteo.com](https://open-meteo.com/), licensed under [CC BY 4.0](https://creativecommons.org/licenses/by/4.0/)

## License

This project is licensed under the MIT License.
