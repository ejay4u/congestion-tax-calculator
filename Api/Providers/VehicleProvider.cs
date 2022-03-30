using System;
using Common;

namespace Api.Providers
{
    public class VehicleProvider : IVehicleProvider
    {
        public Vehicle FindVehicle(string vehicleRegistration)
        {
            var vehicleTypes = new[] { "car", "motorcycle", "bus", "foreign", "military", "emergency", "diplomat" };

            var randomNumber = new Random();
            var vehicleIndex = randomNumber.Next(0, 6);

            return new Vehicle(Guid.NewGuid(), vehicleTypes[vehicleIndex]);
        }
    }
}