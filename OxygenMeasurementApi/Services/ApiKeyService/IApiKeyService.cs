namespace OxygenMeasurementApi.Services.ApiKeyService;

/// <summary>
/// Interface for validating API keys associated with Oxygen Measurement Systems.
/// </summary>
public interface IApiKeyService
{
    /// <summary>
    /// Validates the provided API key for a specific Oxygen Measurement System.
    /// </summary>
    /// <param name="systemId">The identifier of the Oxygen Measurement System.</param>
    /// <param name="apiKey">The API key to validate.</param>
    /// <returns>True if the API key is valid for the specified system; otherwise, false.</returns>
    Task<bool> ValidateApiKey(int systemId, string apiKey);
}
