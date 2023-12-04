using OxygenMeasurementApi.Authorization;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Mappers;

public static class RequestToDbEntityMapper
{
    public static OxygenMeasurementSystem ToDbEntity(this AddOxygenMeasurementSystemRequestDto systemRequestDto)
    {
        return new OxygenMeasurementSystem
        {
            SystemName = systemRequestDto.SystemName,
            Zipcode = systemRequestDto.Zipcode,
            Location = systemRequestDto.Location,
            SystemNotificationAdvisors = systemRequestDto.SystemNotificationAdvisors.ToDbEntityList(),
            ApiKey = new ApiKey
            {
                ApiKeyValue = ApiKeyGenerator.GenerateNewApiKey(15)
            }
        };
    }
    
    public static List<SystemNotificationAdvisor> ToDbEntityList(this IEnumerable<SystemNotificationAdvisorDto> systemAdvisors)
    {
        return systemAdvisors.Select(systemAdvisorDto => systemAdvisorDto.ToDbEntity()).ToList();
    }

    public static SystemNotificationAdvisor ToDbEntity(this SystemNotificationAdvisorDto systemNotificationAdvisorDto)
    {
        return new SystemNotificationAdvisor
        {
            Email = systemNotificationAdvisorDto.Email
        };
    }

    public static OxygenMeasurement ToDbEntity(this AddOxygenMeasurementRequestDto requestDto)
    {
        return new OxygenMeasurement
        {
            OxygenValue = requestDto.OxygenValue,
            MeasurementTime = DateTime.Now.ToUniversalTime(),
            OxygenMeasurementSystemId = requestDto.OxygenMeasurementSystemId
        };
    }
}