using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using Api.Providers;
using congestion.calculator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api.ControllerHandlers
{
    public class CongestionTaxesControllerHandler : ICongestionTaxesControllerHandler
    {
        private readonly ICongestionTaxCalculator congestionTaxCalculator;
        private readonly IVehicleProvider vehicleProvider;

        public CongestionTaxesControllerHandler(IVehicleProvider vehicleProvider,
            ICongestionTaxCalculator congestionTaxCalculator)
        {
            this.vehicleProvider = vehicleProvider;
            this.congestionTaxCalculator = congestionTaxCalculator;
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

            var result = congestionTaxCalculator.GetTax(vehicle, datetime);

            return new OkObjectResult(result);
        }

        public async Task<IStatusCodeActionResult> GetAsync(string vehicleRegistration, DateTime dateTime,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(vehicleRegistration))
                return new BadRequestObjectResult($"{nameof(vehicleRegistration)} must not be empty");

            if (dateTime.Equals(null))
                return new BadRequestObjectResult($"{nameof(dateTime)} must not be empty");

            var vehicle = vehicleProvider.FindVehicle(vehicleRegistration);

            var result = congestionTaxCalculator.GetTax(vehicle, new[] { dateTime });

            return new OkObjectResult(result);
        }
    }
}