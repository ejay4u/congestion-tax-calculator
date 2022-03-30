using Common;

namespace Api.Providers
{
    public interface IVehicleProvider
    {
        Vehicle FindVehicle(string vehicleRegistration);
    }
}