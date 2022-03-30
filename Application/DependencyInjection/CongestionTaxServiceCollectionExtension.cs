using Microsoft.Extensions.DependencyInjection;

namespace congestion.calculator.DependencyInjection
{
    public static class CongestionTaxServiceCollectionExtension
    {
        public static IServiceCollection AddCongestionTaxCalculator(this IServiceCollection services)
        {
            return services.AddSingleton<ICongestionTaxCalculator, CongestionTaxCalculator>();
        }
    }
}