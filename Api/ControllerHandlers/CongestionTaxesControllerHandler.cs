using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api.ControllerHandlers
{
    public class CongestionTaxesControllerHandler : ICongestionTaxesControllerHandler
    {
        public Task<IStatusCodeActionResult> GetAsync(string vehicleRegistration, DateTime[] dateTime,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}