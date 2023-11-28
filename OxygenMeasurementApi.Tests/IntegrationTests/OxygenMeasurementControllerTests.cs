using System.Net;
using System.Net.Http.Json;
using OxygenMeasurementApi.OxygenMeasurements.Create;

namespace OxygenMeasurementApi.Tests.IntegrationTests;

public class OxygenMeasurementControllerTests : BaseIntegrationTest
{
    public OxygenMeasurementControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_new_OxygenMeasurement_shouldReturn_statusCode_201()
    {
        // Arrange
        var newOxygenMeasurement = new CreateOxygenMeasurement(new decimal(4.55));

        // Act
        var response = await HttpClient.PostAsJsonAsync("Oxygen/CreateOxygenMeasurement", newOxygenMeasurement);
        var createdOxygenMeasurementResponse = response.StatusCode;

        // Assert
        Assert.True(createdOxygenMeasurementResponse == HttpStatusCode.Created);
    }
}