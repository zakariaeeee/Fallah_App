using System;
using System.Net.Http;
using System.Threading.Tasks;
using Fallah_App.Service;
using NUnit.Framework;

namespace Fallah_App.Test
{
    [TestFixture]
    public class MeteoTests
    {
      
        [Test]
        public async Task GetMeteo_ValidCoordinates_ReturnsWeatherData()
        {
            String latitude = "12.345";
            String longitude = "67.890";

            Meteo weatherData = await Meteo.getMeteo(latitude, longitude);

            Assert.IsNotNull(weatherData);
          
        }

        [Test]
        public async Task GetMeteo_InvalidCoordinates_ReturnsNull()
        {
            String latitude = "123456789";
            String longitude = "67.890";

            var weatherData = await Meteo.getMeteo(latitude, longitude);

            Assert.IsNull(weatherData);
        }

      
    }
}
