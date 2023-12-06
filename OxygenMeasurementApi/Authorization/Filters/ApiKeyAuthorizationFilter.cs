using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OxygenMeasurementApi.Services.ApiKeyService;

namespace OxygenMeasurementApi.Authorization.Filters;

/// <summary>
/// This class implements the IAsyncAuthorizationFilter interface to perform API key authorization.
/// </summary>

public class ApiKeyAuthorizationFilter : IAsyncAuthorizationFilter
{
    
    // Private property to store the injected IApiKeyService instance.
    private IApiKeyService ApiKeyService { get; }
    // Constructor that takes an IApiKeyService dependency to validate API keys.
    public ApiKeyAuthorizationFilter(IApiKeyService apiKeyService)
    {
        ApiKeyService = apiKeyService;
    }
    

    // 
    /// <summary>
    ///  Method OnAuthorizationAsync This method is called during the authorization process.
    /// </summary>
    /// <param name="context"> the request context</param>
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // Retrieve API key and system ID from request headers.
        context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var apiKeyFromHeader);
        context.HttpContext.Request.Headers.TryGetValue("X-System-Id", out var systemIdFromHeader);

        // Check if API key is missing in the header.
        if (apiKeyFromHeader.Count == 0)
        {
            context.Result = new UnauthorizedObjectResult("Api key value in header X-Api-Key is missing");
            return;
        }

        // Check if system ID is missing in the header.
        if (systemIdFromHeader.Count == 0)
        {
            context.Result = new UnauthorizedObjectResult("System id key value in header X-System-Id is missing");
            return;
        }

        // Check if both API key and system ID are missing in the header.
        if (apiKeyFromHeader.Count == 0 && systemIdFromHeader.Count == 0)
        {
            context.Result =
                new UnauthorizedObjectResult(
                    "System id key value in header X-System-Id and api key value in header X-Api-Key is missing");
            return;
        }

        // Parse the system ID from the header and check if it is a valid integer.
        if (!int.TryParse(systemIdFromHeader, out var systemId))
        {
            context.Result =
                new UnauthorizedObjectResult($"X-System-Id value of : {systemId} could not be parsed as integer");
            return;
        }

        // Check if the API key is empty or null.
        if (string.IsNullOrEmpty(apiKeyFromHeader) || string.IsNullOrWhiteSpace(apiKeyFromHeader))
        {
            context.Result = new UnauthorizedObjectResult("the value of X-Api-Key cannot be empty or null");
            return;
        }

        // Validate the API key using the injected IApiKeyService.
        bool isValidApiKey = await ApiKeyService.ValidateApiKey(systemId, apiKeyFromHeader.ToString());

        // If the API key is not valid, set the result to Unauthorized.
        if (!isValidApiKey)
        {
            context.Result = new UnauthorizedObjectResult("Api key is not valid");
        }
    }
}