namespace OxygenMeasurementApi.Exceptions;

public class CustomArgumentOutOfRangeException : ArgumentOutOfRangeException
{
    public override string ParamName { get; }
    public override object ActualValue { get; }

    public CustomArgumentOutOfRangeException(string paramName, object actualValue, string message)
        : base($"{paramName} {message}")
    {
        ParamName = paramName;
        ActualValue = actualValue;
    }
}