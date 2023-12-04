using OxygenMeasurementApi.Data.Dtos.ResponseDtos;
using OxygenMeasurementApi.Exceptions;
using OxygenMeasurementApi.Tests.MockTestData;

namespace OxygenMeasurementApi.Tests.UnitTests;

// BaseIntegrationTest makes sure to instantiate a new container and make the database connection
public class OxygenMeasurementServiceTests : BaseUnitTest
{
    public OxygenMeasurementServiceTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task should_Add_New_OxygenMeasurement_if_oxygenMeasurementSystem_Exists()
    {
        // Arrange
        // Insert test system
        var testSystem = TestDataProvider.GetTestOxygenMeasurementSystemRequestDto();
        var testSystemInserted = await OxygenMeasurementSystemService.AddOxygenMeasurementSystemAsync(testSystem);

        if (testSystemInserted != null)
        {
            var newOxygenMeasurement = TestDataProvider.GetTestOxygenMeasurementRequestDto(testSystemInserted.Id);

            // Act
            var actual = await OxygenMeasurementService.AddOxygenMeasurementAsync(newOxygenMeasurement);

            // Assert
            Assert.NotNull(actual);
            Assert.IsType<OxygenMeasurementResponseDto>(actual);
            Assert.True(actual != null && actual.Id > 0);
            Assert.True(actual != null && newOxygenMeasurement.OxygenValue == actual.OxygenValue);
        }
    }

    [Fact]
    public async Task when_add_oxygenMeasurement_should_throw_notFoundException_if_oxygenMeasurementSystem_does_notExists()
    {
        // Arrange
        const int notValidSystemId = 999;
        var oxygenMeasurementRequestDto = TestDataProvider.GetTestOxygenMeasurementRequestDto(notValidSystemId);

        // Actual
        var actual = await Assert.ThrowsAsync<NotFoundException>(async () =>
            await OxygenMeasurementService.AddOxygenMeasurementAsync(oxygenMeasurementRequestDto));

        // Assert
        Assert.Contains("not found", actual.Message);
    }
    
    
}