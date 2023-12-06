using OxygenMeasurementApi.Data.Dtos.RequestDtos;

namespace OxygenMeasurementApi.Tests.MockTestData;

public static class TestDataProvider
{
    public static AddOxygenMeasurementSystemRequestDto GetTestOxygenMeasurementSystemRequestDto()
    {
        return new AddOxygenMeasurementSystemRequestDto
        {
            SystemName = "test",
            Location = "ringsted",
            SystemNotificationAdvisors = new List<AddSystemNotificationAdvisorRequestDto>
            {
                new()
                {
                    Email = "test@test.dk"
                }
            }
        };
    }

    public static AddOxygenMeasurementRequestDto GetTestOxygenMeasurementRequestDto(int systemId)
    {
        return new AddOxygenMeasurementRequestDto
        {
            OxygenValue = (decimal)4.44,
            OxygenMeasurementSystemId = systemId
        };
    }
}