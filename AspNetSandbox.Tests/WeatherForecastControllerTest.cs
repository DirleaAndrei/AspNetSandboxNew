using AspNetSendbox;
using AspNetSendbox.Controllers;
using System;
using Xunit;

namespace AspNetSandbox.Tests
{
    public class WeatherForecastControllerTest
    {
        [Fact]
        public void ConvertResponseToWeatherForecastTest()
        {
            // Assume
            string content = "";
            var controller = new WeatherForecastController();

            // Act
            var output = controller.ConvertResponseToWeatherForecast(content);

            // Assert
            Assert.Equal("rainy", ((WeatherForecast[])output)[0].Summary);
        }
    }
}
