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
            string content = LoadJsonFromResource();
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
            string content = LoadJsonFromResource();
            var controller = new WeatherForecastController();

            // Act
            var output = controller.ConvertResponseToWeatherForecast(content);

            // Assert
            var weatherForecastForAfterTomorow = ((WeatherForecast[])output)[1];
            Assert.Equal("Clear", weatherForecastForAfterTomorow.Summary);
            Assert.Equal(20, weatherForecastForAfterTomorow.TemperatureC);
            Assert.Equal(new DateTime(2021, 9, 4), weatherForecastForAfterTomorow.Date);
        }
        private string LoadJsonFromResource()
        {
            var assembly = this.GetType().Assembly;
            var assemblyName = assembly.GetName().Name;
            var resourceName = $"{assemblyName}.DataFromOpenWeatherAPI.json";
            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            using (var tr = new StreamReader(resourceStream))
            {
                return tr.ReadToEnd();
            }
        }
    }
    
}
