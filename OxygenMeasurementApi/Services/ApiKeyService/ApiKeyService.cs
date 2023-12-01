using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi.Data.Context;

namespace OxygenMeasurementApi.Services.ApiKeyService;

public class ApiKeyService : IApiKeyService
{
    private IOxygenDbContext OxygenDbContext { get; }
    
    public ApiKeyService(IOxygenDbContext oxygenDbContext)
    {
        OxygenDbContext = oxygenDbContext;
    }

    public async Task<bool> ValidateApiKey(int systemId, string apiKey)
    {
        bool isValid = false;

        if (systemId <= 0 || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiKey))
        {
            return isValid;
        }

        var apiKeyFromDb = await OxygenDbContext.ApiKeys.FirstOrDefaultAsync(key =>
            key.ApiKeyId == apiKey && key.OxygenMeasurementSystem.Id == systemId);

        if (apiKeyFromDb != null)
        {
            isValid = true;
        }

        return isValid;
    }
}