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
        // Post the test system to the endpoint for creating oxygen measurement systems
        var testSystemResponse = await HttpClient.PostAsJsonAsync("api/OxygenSystem/PostOxygenMeasurementSystem", testSystem);
        // Parse the response into an object for further usage
        var parsedTestSystemResponse = await testSystemResponse.Content.ReadFromJsonAsync<OxygenMeasurementSystemResponseDto>();

        // Check if the parsing was successful
        if (parsedTestSystemResponse != null)
        {
            // Set request headers with the API key and system ID for subsequent requests
            HttpClient.SetRequestHeaders(parsedTestSystemResponse.ApiKeyValue, parsedTestSystemResponse.Id);
            var newOxygenMeasurement = TestDataProvider.GetTestOxygenMeasurementRequestDto(parsedTestSystemResponse.Id);

            // Act
            // Post the new oxygen measurement to the endpoint
            var response = await HttpClient.PostAsJsonAsync("api/Oxygen/PostOxygenMeasurement", newOxygenMeasurement);

            // Get the status code from the response
            var createdOxygenMeasurementResponse = await response.Content.ReadFromJsonAsync<OxygenMeasurementResponseDto>();

            // Assert
            // Check that the status code is 201 (Created)
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            
            // Check that the response content is not null
            Assert.NotNull(createdOxygenMeasurementResponse);

            Assert.True(newOxygenMeasurement.OxygenValue == createdOxygenMeasurementResponse.OxygenValue);
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