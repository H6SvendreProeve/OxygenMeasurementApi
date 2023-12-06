using System.ComponentModel.DataAnnotations;

namespace OxygenMeasurementApi.Data.AttributeValidators;

/// <summary>
/// Custom validation attribute to ensure that an integer value is greater than zero.
/// </summary>
public class GreaterThanZeroAttribute : ValidationAttribute
{
    /// <summary>
    /// Determines whether the specified value is greater than zero.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>
    ///   <c>true</c> if the value is an integer greater than zero; otherwise <c>false</c>.
    /// </returns>
    public override bool IsValid(object? value)
    {
        if (value is int intValue)
        {
            return intValue > 0;
        }

        return false;
    }
}