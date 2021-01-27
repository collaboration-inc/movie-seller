using NUnit.Framework;
using FluentAssertions;
using MovieSeller.Controllers;
using Moq;
using Microsoft.Extensions.Logging;

namespace MovieSeller.Tests
{
    public class WeatherForecastControllerTests
    {
        private WeatherForecastController _controller;

        [SetUp]
        public void Setup()
        {
            var loggerMock = new Mock<ILogger<WeatherForecastController>>();
            _controller = new WeatherForecastController(loggerMock.Object);
        }

        [Test]
        public void PassingTest()
        {
            _controller.Invoking(x => x.Get())
                .Should().NotThrow();
        }
    }
}
