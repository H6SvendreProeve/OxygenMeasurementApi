using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Tests.MockTestData;

public static class TestDataProvider
{
    public static AddOxygenMeasurementSystemRequestDto GetTestOxygenMeasurementSystemRequestDto()
    {
        return new AddOxygenMeasurementSystemRequestDto
        {
            SystemName = "test",
            Zipcode = "4100",
            Location = "ringsted",
            SystemNotificationAdvisors = new List<SystemNotificationAdvisorDto>
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