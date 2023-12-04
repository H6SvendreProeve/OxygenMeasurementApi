using System.ComponentModel.DataAnnotations;

namespace OxygenMeasurementApi.Data.AttributeValidators;

public class GreaterThanZeroAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is int intValue)
        {
            return intValue > 0;
        }

        return false;
    }
}