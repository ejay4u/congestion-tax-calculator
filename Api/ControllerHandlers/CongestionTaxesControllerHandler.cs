using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using Api.Vehicle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api.ControllerHandlers
{
    public class CongestionTaxesControllerHandler : ICongestionTaxesControllerHandler
    {
        private readonly IVehicleProvider vehicleProvider;

        public CongestionTaxesControllerHandler(IVehicleProvider vehicleProvider)
        {
            this.vehicleProvider = vehicleProvider;
        }


        public async Task<IStatusCodeActionResult> GetAsync(CongestionTaxInputModel congestionTaxInputModel)
        {
            if (string.IsNullOrEmpty(congestionTaxInputModel.VehicleRegistration))
                return new BadRequestObjectResult(
                    $"{nameof(congestionTaxInputModel.VehicleRegistration)} must not be empty");

            if (congestionTaxInputModel.Timestamp.Equals(null))
                return new BadRequestObjectResult($"{nameof(congestionTaxInputModel.Timestamp)} must not be empty");

            var datetime = Array.ConvertAll(congestionTaxInputModel.Timestamp, DateTime.Parse);

            var vehicle = vehicleProvider.FindVehicle(congestionTaxInputModel.VehicleRegistration);

            return new OkObjectResult(60);
        }

        public async Task<IStatusCodeActionResult> GetAsync(string vehicleRegistration, DateTime dateTime,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(vehicleRegistration))
                return new BadRequestObjectResult($"{nameof(vehicleRegistration)} must not be empty");

            if (dateTime.Equals(null))
                return new BadRequestObjectResult($"{nameof(dateTime)} must not be empty");

            var vehicle = vehicleProvider.FindVehicle(vehicleRegistration);

            return new OkObjectResult(60);
        }
    }
}