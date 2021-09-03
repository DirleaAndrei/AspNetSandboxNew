using AspNetSendbox;
using AspNetSendbox.Controllers;
using System;
using System.IO;
using Xunit;

namespace AspNetSandbox.Tests
{
    public class WeatherForecastControllerTest
    {
        [Fact]
        public void ConvertResponseToWeatherForecastTest()
        {
            // Assume
            string content = LoadJsonFromResource("DataFromOpenWeatherAPI.json");
            var controller = new WeatherForecastController();

            // Act
            var output = controller.ConvertResponseToWeatherForecast(content);

            // Assert
            var weatherForecastForTomorow = ((WeatherForecast[])output)[0];
            Assert.Equal("Clear", weatherForecastForTomorow.Summary);
            Assert.Equal(18, weatherForecastForTomorow.TemperatureC);
            Assert.Equal(new DateTime(2021,9,3), weatherForecastForTomorow.Date);
        }

        [Fact]
        public void ConvertResponseToWeatherForecastAfterTomorrowTest()
        {
            // Assume
            string content = LoadJsonFromResource("DataFromOpenWeatherAPI.json");
            var controller = new WeatherForecastController();

            // Act
            var output = controller.ConvertResponseToWeatherForecast(content);

            // Assert
            var weatherForecastForAfterTomorow = ((WeatherForecast[])output)[1];
            Assert.Equal("Clear", weatherForecastForAfterTomorow.Summary);
            Assert.Equal(20, weatherForecastForAfterTomorow.TemperatureC);
            Assert.Equal(new DateTime(2021, 9, 4), weatherForecastForAfterTomorow.Date);
        }

        [Fact]
        public void GetCityCoordinatesFromOpenWeatherTest()
        {
            // Assume
            string content = LoadJsonFromResource("DataFromOpenWeatherApiWithCityCoordinates.json");
            var controller = new WeatherForecastController();

            // Act
            var output = controller.GetCityCoordinatesFromOpenWeather(content);

            // Assert
            var weatherForecastCityCoordinates = ((WeatherForecastCityCoordinates)output);
            Assert.Equal(26, weatherForecastCityCoordinates.Longitude);
            Assert.Equal(44, weatherForecastCityCoordinates.Latitude);
        }
        private string LoadJsonFromResource(string jsonResponseOfApiFile)
        {
            var assembly = this.GetType().Assembly;
            var assemblyName = assembly.GetName().Name;
            var resourceName = $"{assemblyName}.{jsonResponseOfApiFile}";
            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            using (var tr = new StreamReader(resourceStream))
            {
                return tr.ReadToEnd();
            }
        }
    }
    
}
