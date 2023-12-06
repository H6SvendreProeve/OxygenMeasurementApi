using System.Data.Common;

namespace OxygenMeasurementApi.Exceptions;

/// <summary>
/// Represents a custom exception for database-related errors.
/// </summary>
public class CustomDbException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomDbException"/> class with the specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public CustomDbException(string? message) : base(message)
    {
       
    }
}