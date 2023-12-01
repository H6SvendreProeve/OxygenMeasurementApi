using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OxygenMeasurementApi.Services.ApiKeyService;

namespace OxygenMeasurementApi.Authorization.Filters;

public class ApiKeyAuthorizationFilter : IAsyncAuthorizationFilter
{
    public ApiKeyAuthorizationFilter(IApiKeyService apiKeyService)
    {
        ApiKeyService = apiKeyService;
    }

    private IApiKeyService ApiKeyService { get; }


    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var apiKeyFromHeader);
        context.HttpContext.Request.Headers.TryGetValue("X-System-Id", out var systemIdFromHeader);

        if (apiKeyFromHeader.Count == 0)
        {
            context.Result = new UnauthorizedObjectResult("Api key value in header X-Api-Key is missing");
            return;
        }

        if (systemIdFromHeader.Count == 0)
        {
            context.Result = new UnauthorizedObjectResult("System id key value in header X-System-Id is missing");
            return;
        }

        if (apiKeyFromHeader.Count == 0 && systemIdFromHeader.Count == 0)
        {
            context.Result = new UnauthorizedObjectResult("System id key value in header X-System-Id and api key value in header X-Api-Key is missing");
            return;
        }

        if (!int.TryParse(systemIdFromHeader, out var systemId))
        {
            context.Result =
                new UnauthorizedObjectResult($"X-System-Id value of : {systemId} could not be parsed as integer");
            return;
        }

        if (string.IsNullOrEmpty(apiKeyFromHeader) || string.IsNullOrWhiteSpace(apiKeyFromHeader))
        {
            context.Result = new UnauthorizedObjectResult("the value of X-Api-Key cannot be empty or null");
            return;
        }

        bool isValidApiKey = await ApiKeyService.ValidateApiKey(systemId, apiKeyFromHeader.ToString());

        if (!isValidApiKey)
        {
            context.Result = new UnauthorizedObjectResult("Api key is not valid");
        }
    }
}