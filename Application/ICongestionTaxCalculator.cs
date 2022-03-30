using System;
using Common;

namespace congestion.calculator
{
    public interface ICongestionTaxCalculator
    {
        int GetTax(Vehicle vehicle, DateTime[] dates);

        int GetTollFee(DateTime date, Vehicle vehicle);
    }
}