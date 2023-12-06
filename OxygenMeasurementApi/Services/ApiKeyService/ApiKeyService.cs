using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Context;

namespace OxygenMeasurementApi.Services.ApiKeyService;

/// <summary>
/// Service class for validating API keys associated with Oxygen Measurement Systems.
/// </summary>
public class ApiKeyService : IApiKeyService
{
    private readonly IOxygenDbContext oxygenDbContext;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiKeyService"/> class.
    /// </summary>
    /// <param name="dbContext">The Oxygen Measurement Database context.</param>
    public ApiKeyService(IOxygenDbContext dbContext)
    {
        oxygenDbContext = dbContext;
    }

    /// <summary>
    /// Validates the provided API key for a specific Oxygen Measurement System.
    /// </summary>
    /// <param name="systemId">The identifier of the Oxygen Measurement System.</param>
    /// <param name="apiKey">The API key to validate.</param>
    /// <returns>True if the API key is valid for the specified system; otherwise, false.</returns>
    public async Task<bool> ValidateApiKey(int systemId, string apiKey)
    {
        bool isValid = false;

        if (systemId <= 0 || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiKey))
        {
            return isValid;
        }

        var apiKeyFromDb = await oxygenDbContext.ApiKeys.FirstOrDefaultAsync(key =>
            key.ApiKeyValue == apiKey  && key.OxygenMeasurementSystem.OxygenMeasurementSystemId == systemId);

        if (apiKeyFromDb != null)
        {
            isValid = true;
        }

        return isValid;
    }
}