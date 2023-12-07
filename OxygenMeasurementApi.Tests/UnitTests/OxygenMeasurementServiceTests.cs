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
    public async Task Should_Add_New_OxygenMeasurement_If_OxygenMeasurementSystem_Exists()
    {
        // Arrange
        // Insert a test oxygen measurement system
        var testSystem = TestDataProvider.GetTestOxygenMeasurementSystemRequestDto();
        var testSystemInserted = await OxygenMeasurementSystemService.AddOxygenMeasurementSystemAsync(testSystem);

        // Check if the test system was successfully inserted
        if (testSystemInserted != null)
        {
            // Create a new test oxygen measurement associated with the inserted system
            var newOxygenMeasurement = TestDataProvider.GetTestOxygenMeasurementRequestDto(testSystemInserted.Id);

            // Act
            // Add the new oxygen measurement
            var actual = await OxygenMeasurementService.AddOxygenMeasurementAsync(newOxygenMeasurement);

            // Assert
            // Verify that the returned object is not null
            Assert.NotNull(actual);

            // Verify that the returned object is of the correct type
            Assert.IsType<OxygenMeasurementResponseDto>(actual);

            // Verify that the returned object has a valid ID
            Assert.True(actual != null && actual.Id > 0);

            // Verify that the returned object has the correct oxygen value
            Assert.True(actual != null && newOxygenMeasurement.OxygenValue == actual.OxygenValue);
        }
        else
        {
            // assert a failure if the test system insertion fails
            Assert.True(false, "Failed to insert the test oxygen measurement system.");
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