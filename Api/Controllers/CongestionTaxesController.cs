using System;
using System.Threading.Tasks;
using Api.ControllerHandlers;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongestionTaxesController : Controller
    {
        private readonly ICongestionTaxesControllerHandler congestionTaxesControllerHandler;

        public CongestionTaxesController(ICongestionTaxesControllerHandler congestionTaxesControllerHandler)
        {
            this.congestionTaxesControllerHandler = congestionTaxesControllerHandler;
        }

        /// <summary>
        ///     Creates request to calculate total congestion taxes for multiple timestamps.
        /// </summary>
        /// <param name="congestionTaxInputModel">The <see cref="CongestionTaxInputModel" /> to create.</param>
        /// <returns>The appropriate status code from one of the possible <see cref="StatusCodes" />.</returns>
        [HttpGet]
        public Task<IStatusCodeActionResult> GetAsync(CongestionTaxInputModel congestionTaxInputModel)
        {
            return congestionTaxesControllerHandler.GetAsync(congestionTaxInputModel);
        }

        /// <summary>
        ///     Creates request to calculate tax for single timestamp.
        /// </summary>
        /// <param name="vehicleRegistration">The vehicle registration number.</param>
        /// <param name="timestamp">The timestamp for period of tax calculation</param>
        /// <returns>The appropriate status code from one of the possible <see cref="StatusCodes" />.</returns>
        [HttpGet("{vehicleRegistration}/{timestamp}")]
        public Task<IStatusCodeActionResult> GetAsync(string vehicleRegistration, DateTime timestamp)
        {
            return congestionTaxesControllerHandler.GetAsync(vehicleRegistration, timestamp,
                HttpContext.RequestAborted);
        }
    }
}