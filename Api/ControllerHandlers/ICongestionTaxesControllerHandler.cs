using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api.ControllerHandlers
{
    public interface ICongestionTaxesControllerHandler
    {
        Task<IStatusCodeActionResult> GetAsync(string vehicleRegistration, DateTime[] dateTime,
            CancellationToken cancellationToken);
    }
}