using System;
using congestion.calculator;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Congestion_tax_calculator.UnitTests
{
    public class CongestionTaxCalculatorTests
    {
        [Theory]
        [InlineData("2013-01-14 21:00:00", 0)]
        [InlineData("2013-01-15 21:00:00", 0)]
        [InlineData("2013-02-07 06:23:27", 8)]
        [InlineData("2013-02-07 15:27:00", 13)]
        [InlineData("2013-02-08 06:27:00", 8)]
        [InlineData("2013-02-08 06:20:27", 8)]
        [InlineData("2013-02-08 14:35:00", 8)]
        [InlineData("2013-02-08 15:29:00", 13)]
        [InlineData("2013-02-08 15:47:00", 18)]
        [InlineData("2013-02-08 16:01:00", 18)]
        [InlineData("2013-02-08 16:48:00", 18)]
        [InlineData("2013-02-08 17:49:00", 13)]
        [InlineData("2013-02-08 18:29:00", 8)]
        [InlineData("2013-02-08 18:35:00", 0)]
        [InlineData("2013-03-26 14:25:00", 8)]
        [InlineData("2013-03-28 14:07:27", 0)]
        public void GetTax_GivenNonExemptVehicle_ShouldReturnTax(string datetime, int expectedTax)
        {
            // Arrange
            var car = new Car();
            var congestionTaxCalculator = new CongestionTaxCalculator();

            // Act
            var result = congestionTaxCalculator.GetTax(car, new[] { DateTime.Parse(datetime) });

            // Assert
            result.Should().Be(expectedTax);
        }

        [Theory]
        [InlineData("2013-01-14 21:00:00", 0)]
        [InlineData("2013-02-08 06:27:00", 0)]
        [InlineData("2013-02-08 15:29:00", 0)]
        [InlineData("2013-02-08 16:48:00", 0)]
        public void GetTax_GivenTaxExemptVehicle_ShouldReturnZero(string datetime, int expectedTax)
        {
            // Arrange
            var vehicle = A.Fake<Vehicle>();
            A.CallTo(() => vehicle.GetVehicleType()).ReturnsLazily(() => "Tractor");
            var congestionTaxCalculator = new CongestionTaxCalculator();

            // Act
            var result = congestionTaxCalculator.GetTax(vehicle, new[] { DateTime.Parse(datetime) });

            // Assert
            result.Should().Be(expectedTax);
        }
    }
}