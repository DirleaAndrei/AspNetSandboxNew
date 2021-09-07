using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetSandbox;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AspNetSendbox.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private const float KELVIN_CONST = 273.15f;

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var client = new RestClient("https://api.openweathermap.org/data/2.5/onecall?lat=45.657974&lon=25.601198&exclude=hourly,minutely&appid=5705b52fdd12e0753d98f978798de52a");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);


            return ConvertResponseToWeatherForecast(response.Content);

        }

        [HttpGet]
        [Route("City")]
        public WeatherForecastCityCoordinates GetCityCoordinates(string City)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/weather?q={City}&appid=5705b52fdd12e0753d98f978798de52a");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            return GetCityCoordinatesFromOpenWeather(response.Content);

        }


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
                    Summary = weatherSummary
                };
            })
            .ToArray();
        }


        [NonAction]
        public WeatherForecastCityCoordinates GetCityCoordinatesFromOpenWeather(string content)
        {

            var json = JObject.Parse(content);

            var jsonCoords = json["coord"];

            return new WeatherForecastCityCoordinates
            {
                Latitude = jsonCoords.Value<int>("lat"),
                Longitude = jsonCoords.Value<int>("lon")
            };

        }

        private static int ExtractCelsiusTemperatureFromDailyWeather(JToken jsonDailyForecast)
        {
            return (int)Math.Round(jsonDailyForecast["temp"].Value<float>("day") - KELVIN_CONST);
        }
    }
}
