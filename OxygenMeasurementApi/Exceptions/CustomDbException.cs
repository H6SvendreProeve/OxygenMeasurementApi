using System.Data.Common;

namespace OxygenMeasurementApi.Exceptions;

public class CustomDbException : Exception
{
    public CustomDbException(string? message) : base(message)
    {
       
    }
}