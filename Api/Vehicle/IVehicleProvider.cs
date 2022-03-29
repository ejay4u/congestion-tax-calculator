namespace Api.Vehicle
{
    public interface IVehicleProvider
    {
        Vehicle FindVehicle(string vehicleRegistration);
    }
}