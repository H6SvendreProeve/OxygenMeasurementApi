using Microsoft.Extensions.DependencyInjection;
using OxygenMeasurementApi.Services.OxygenMeasurementService;

namespace OxygenMeasurementApi.Tests.UnitTests;

// iClassFixtures is instantiated before every test.
// Meaning a new instance of IntegrationTestWebAppFactory is instantiated which means that we get 1 container for every class that instantiates BaseIntegrationTest
public abstract class BaseUnitTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly IOxygenMeasurementService OxygenMeasurementService;
    
    protected BaseUnitTest(IntegrationTestWebAppFactory factory)
    {
 
        var scope = factory.Services.CreateScope();

        OxygenMeasurementService = scope.ServiceProvider.GetRequiredService<IOxygenMeasurementService>();
   
    }
}