using OxygenMeasurementApi.OxygenMeasurements.Create;

namespace OxygenMeasurementApi.Tests.UnitTests;

// BaseIntegrationTest makes sure to instantiate a new container and make the database connection
public class OxygenMeasurementServiceTests : BaseUnitTest
{
    public OxygenMeasurementServiceTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_shouldAdd_New_OxygenMeasurement()
    {
        // Arrange
        var newOxygenMeasurement = new CreateOxygenMeasurement(new decimal(4.44));

        // Act
        var actual = await OxygenMeasurementService.CreateOxygenMeasurement(newOxygenMeasurement);
        
        // Assert
        Assert.True(actual);
    }  
}