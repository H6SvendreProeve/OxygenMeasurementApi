using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Context;

namespace OxygenMeasurementApi.Services.ApiKeyService;

public class ApiKeyService : IApiKeyService
{
    private readonly IOxygenDbContext oxygenDbContext;
    
    public ApiKeyService(IOxygenDbContext dbContext)
    {
        oxygenDbContext = dbContext;
    }

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