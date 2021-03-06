using System;
using Common;

namespace congestion.calculator
{
    public class CongestionTaxCalculator : ICongestionTaxCalculator
    {
        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */
        public int GetTax(Vehicle vehicle, DateTime[] dates)
        {
            var intervalStart = dates[0];
            var totalFee = 0;
            foreach (var date in dates)
            {
                var nextFee = GetTollFee(date, vehicle);
                var tempFee = GetTollFee(intervalStart, vehicle);

                long diffInMillies = date.Millisecond - intervalStart.Millisecond;
                var minutes = diffInMillies / 1000 / 60;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }

            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        public int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            var hour = date.Hour;
            var minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            if (hour == 8 && minute >= 30 || hour <= 14 && minute <= 59) return 8;
            if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
            if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            return 0;
        }

        private bool IsTollFreeVehicle(Vehicle vehicle)
        {
            if (vehicle == null) return false;
            var vehicleType = vehicle.VehicleType;
            return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString(), StringComparison.OrdinalIgnoreCase) ||
                   vehicleType.Equals(TollFreeVehicles.Bus.ToString(), StringComparison.OrdinalIgnoreCase) ||
                   vehicleType.Equals(TollFreeVehicles.Emergency.ToString(), StringComparison.OrdinalIgnoreCase) ||
                   vehicleType.Equals(TollFreeVehicles.Diplomat.ToString(), StringComparison.OrdinalIgnoreCase) ||
                   vehicleType.Equals(TollFreeVehicles.Foreign.ToString(), StringComparison.OrdinalIgnoreCase) ||
                   vehicleType.Equals(TollFreeVehicles.Military.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        private bool IsTollFreeDate(DateTime date)
        {
            var year = date.Year;
            var month = date.Month;
            var day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            if (year == 2013)
                if (month == 1 && day == 1 ||
                    month == 3 && (day == 28 || day == 29) ||
                    month == 4 && (day == 1 || day == 30) ||
                    month == 5 && (day == 1 || day == 8 || day == 9) ||
                    month == 6 && (day == 5 || day == 6 || day == 21) ||
                    month == 7 ||
                    month == 11 && day == 1 ||
                    month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
                    return true;
            return false;
        }

        private enum TollFreeVehicles
        {
            Motorcycle = 0,
            Bus = 1,
            Emergency = 2,
            Diplomat = 3,
            Foreign = 4,
            Military = 5
        }
    }
}