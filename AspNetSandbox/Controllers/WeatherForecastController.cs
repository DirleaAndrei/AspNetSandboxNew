using System;
using System.Collections.Generic;
using System.Linq;
using AspNetSandbox;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AspNetSendbox.Controllers
{
    /// <summary>
    /// Controller that allows us to get weather forecast.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private const float KELVINCONST = 273.15f;

        /// <summary>
        /// Return weather forecast for 5 days.
        /// </summary>
        /// <returns>
        /// Enumerable of weather forecast objects.
        /// </returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var client = new RestClient("https://api.openweathermap.org/data/2.5/onecall?lat=45.657974&lon=25.601198&exclude=hourly,minutely&appid=5705b52fdd12e0753d98f978798de52a")
            {
                Timeout = -1,
            };
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            return ConvertResponseToWeatherForecast(response.Content);
        }

        /// <summary>
        /// Return City coordinates.
        /// </summary>
        /// <returns>
        /// WeatherForecastCityCoordinates object.
        /// </returns>
        /// <param name="city"></param>
        [HttpGet]
        [Route("city")]
        public WeatherForecastCityCoordinates GetCityCoordinates(string city)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=5705b52fdd12e0753d98f978798de52a")
            {
                Timeout = -1,
            };
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            return GetCityCoordinatesFromOpenWeather(response.Content);
        }

        /// <summary>
        /// Convert response to weather forecast.
        /// </summary>
        /// <returns>
        /// Enumerable of weather forecast objects.
        /// </returns>
        /// <param name="content"></param>
        /// <param name="days"></param>
        [NonAction]
        public IEnumerable<WeatherForecast> ConvertResponseToWeatherForecast(string content, int days = 5)
        {
            var json = JObject.Parse(content);

            var rng = new Random();
            return Enumerable.Range(1, days).Select(index =>
            {
                var jsonDailyForecast = json["daily"][index];
                var unixDateTime = jsonDailyForecast.Value<long>("dt");
                var weatherSummary = jsonDailyForecast["weather"][0].Value<string>("main");

                return new WeatherForecast
                {
                    Date = DateTimeOffset.FromUnixTimeSeconds(unixDateTime).Date,
                    TemperatureC = ExtractCelsiusTemperatureFromDailyWeather(jsonDailyForecast),
                    Summary = weatherSummary,
                };
            })
            .ToArray();
        }

        /// <summary>
        /// Get city coordinates from OpenWeather.
        /// </summary>
        /// <returns>
        /// WeatherForecastCityCoordinates object.
        /// </returns>
        /// <param name="content"></param>
        [NonAction]
        public WeatherForecastCityCoordinates GetCityCoordinatesFromOpenWeather(string content)
        {
            var json = JObject.Parse(content);

            var jsonCoords = json["coord"];

            return new WeatherForecastCityCoordinates
            {
                Latitude = jsonCoords.Value<int>("lat"),
                Longitude = jsonCoords.Value<int>("lon"),
            };
        }

        private static int ExtractCelsiusTemperatureFromDailyWeather(JToken jsonDailyForecast)
        {
            return (int)Math.Round(jsonDailyForecast["temp"].Value<float>("day") - KELVINCONST);
        }
    }
}
