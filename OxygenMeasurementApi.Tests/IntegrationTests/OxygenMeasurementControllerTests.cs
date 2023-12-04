using System.Net;
using System.Net.Http.Json;
using OxygenMeasurementApi.Data.Dtos.ResponseDtos;
using OxygenMeasurementApi.Tests.MockTestData;

namespace OxygenMeasurementApi.Tests.IntegrationTests;

public class OxygenMeasurementControllerTests : BaseIntegrationTest
{
    public OxygenMeasurementControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Add_OxygenMeasurement_shouldReturn_statusCode_201_if_OxygenMeasurementSystem_exists()
    {
        // Arrange
        // Insert test system
        var testSystem = TestDataProvider.GetTestOxygenMeasurementSystemRequestDto();

        // Act
        var testSystemResponse = await HttpClient.PostAsJsonAsync("api/OxygenSystem/PostOxygenMeasurementSystem", testSystem);
        var parsedTestSystemResponse = await testSystemResponse.Content.ReadFromJsonAsync<OxygenMeasurementSystemResponseDto>();


        if (parsedTestSystemResponse != null)
        {
            HttpClient.SetRequestHeaders(parsedTestSystemResponse.ApiKeyValue, parsedTestSystemResponse.Id);
            var newOxygenMeasurement = TestDataProvider.GetTestOxygenMeasurementRequestDto(parsedTestSystemResponse.Id);

            // Act
            var response = await HttpClient.PostAsJsonAsync("Api/Oxygen/PostOxygenMeasurement", newOxygenMeasurement);
            var createdOxygenMeasurementResponse = response.StatusCode;

            // Assert
            Assert.True(createdOxygenMeasurementResponse == HttpStatusCode.Created);
        }
    }
    
    [Fact]
    public async Task Add_OxygenMeasurementSystem_shouldReturn_statusCode_201()
    {
        // Arrange
        // Insert test system
        var testSystem = TestDataProvider.GetTestOxygenMeasurementSystemRequestDto();

        // Act
        var testSystemResponse = await HttpClient.PostAsJsonAsync("api/OxygenSystem/PostOxygenMeasurementSystem", testSystem);
        var parsedTestSystemResponse = await testSystemResponse.Content.ReadFromJsonAsync<OxygenMeasurementSystemResponseDto>();

        // Assert
        Assert.True(testSystemResponse.StatusCode == HttpStatusCode.Created);
        Assert.True(parsedTestSystemResponse != null && parsedTestSystemResponse.Id > 0);
    }
}