using AirportWeather.Components;

using AirportWeather.Services;

using AirportWeather.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.Configure<WeatherApiOptions>(
    builder.Configuration.GetSection(WeatherApiOptions.SectionName));

builder.Services.AddHttpClient<IWeatherService, WeatherService>((serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<WeatherApiOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl);

    // Option A: the API wants the key in a HEADER (most modern APIs)
    // client.DefaultRequestHeaders.Add("X-Api-Key", options.ApiKey);
});

//builder.Services.AddHttpClient<IWeatherService, WeatherService>(client => 
//{
//    client.BaseAddress = new Uri("https://api.open-meteo.com/");
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
