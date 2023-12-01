namespace OxygenMeasurementApi.Services.ApiKeyService;

public interface IApiKeyService
{
    Task<bool> ValidateApiKey(int systemId, string apiKey);
}
