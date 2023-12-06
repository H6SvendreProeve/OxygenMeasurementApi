namespace OxygenMeasurementApi.Exceptions;

/// <summary>
/// Represents an exception thrown when an argument is outside the allowable range.
/// </summary>
public class CustomArgumentOutOfRangeException : ArgumentOutOfRangeException
{
    public override string ParamName { get; }
    public override object ActualValue { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomArgumentOutOfRangeException"/> class
    /// with the specified parameter name, actual value, and error message.
    /// </summary>
    /// <param name="paramName">The name of the parameter that caused the exception.</param>
    /// <param name="actualValue">The actual value of the parameter that caused the exception.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public CustomArgumentOutOfRangeException(string paramName, object actualValue, string message)
        : base($"{paramName} {message}")
    {
        ParamName = paramName;
        ActualValue = actualValue;
    }
}