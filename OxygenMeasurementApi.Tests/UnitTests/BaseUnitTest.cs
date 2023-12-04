using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using OxygenMeasurementApi.Services.ApiKeyService;
using OxygenMeasurementApi.Services.OxygenMeasurementService;
using OxygenMeasurementApi.Services.OxygenMeasurementSystemService;
using OxygenMeasurementApi.Tests.MockTestData;

namespace OxygenMeasurementApi.Tests.UnitTests;

// iClassFixtures is instantiated before every test.
// Meaning a new instance of IntegrationTestWebAppFactory is instantiated which means that we get 1 container for every class that instantiates BaseIntegrationTest
public abstract class BaseUnitTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly IOxygenMeasurementService OxygenMeasurementService;
    protected readonly IOxygenMeasurementSystemService OxygenMeasurementSystemService;
    protected readonly IApiKeyService ApiKeyService;
    
    protected BaseUnitTest(IntegrationTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();

        OxygenMeasurementService = scope.ServiceProvider.GetRequiredService<IOxygenMeasurementService>();
        OxygenMeasurementSystemService = scope.ServiceProvider.GetRequiredService<IOxygenMeasurementSystemService>();
        ApiKeyService = scope.ServiceProvider.GetRequiredService<IApiKeyService>();
   
    }
}