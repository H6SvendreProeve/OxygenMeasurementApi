using OxygenMeasurementApi.Data.Dtos.ResponseDtos;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Mappers;

public static class DbEntityToDtoResponseMapper
{
    public static OxygenMeasurementSystemResponseDto? ToResponse(this OxygenMeasurementSystem oxygenMeasurementSystem)
    {
        return new OxygenMeasurementSystemResponseDto
        {
            Id = oxygenMeasurementSystem.Id,
            SystemName = oxygenMeasurementSystem.SystemName,
            Zipcode = oxygenMeasurementSystem.Zipcode,
            Location = oxygenMeasurementSystem.Location,
            ApiKeyValue = oxygenMeasurementSystem.ApiKey.ApiKeyValue,
            SystemNotificationAdvisors = oxygenMeasurementSystem.SystemNotificationAdvisors.ToResponseList()
        };
    }

    public static SystemNotificationAdvisorResponseDto ToResponse(this SystemNotificationAdvisor systemNotificationAdvisor)
    {
        return new SystemNotificationAdvisorResponseDto
        {
            Id = systemNotificationAdvisor.Id,
            Email = systemNotificationAdvisor.Email
        };
    }

    public static List<SystemNotificationAdvisorResponseDto> ToResponseList(this IEnumerable<SystemNotificationAdvisor> systemNotificationAdvisors)
    {
        return systemNotificationAdvisors.Select(systemNotificationAdvisor => systemNotificationAdvisor.ToResponse()).ToList();
    }

    public static List<OxygenMeasurementResponseDto?> ToResponseList(this IEnumerable<OxygenMeasurement> dbMeasurements)
    {
        return dbMeasurements.Select(oxygenMeasurement => oxygenMeasurement.ToResponse()).ToList();
    }

    public static OxygenMeasurementResponseDto? ToResponse(this OxygenMeasurement oxygenMeasurement)
    {
        return new OxygenMeasurementResponseDto
        {
            Id = oxygenMeasurement.Id,
            OxygenValue = oxygenMeasurement.OxygenValue,
            MeasurementTime = oxygenMeasurement.MeasurementTime,
            OxygenMeasurementSystemId = oxygenMeasurement.OxygenMeasurementSystemId,
            SystemName = oxygenMeasurement.OxygenMeasurementSystem.SystemName,
            SystemLocation = oxygenMeasurement.OxygenMeasurementSystem.Location,
            SystemZipcode = oxygenMeasurement.OxygenMeasurementSystem.Zipcode
        };
    }
    
    public static OxygenMeasurementSystemsResponseDto? ToSimpleResponse(this OxygenMeasurementSystem oxygenMeasurementSystem)
    {
        return new OxygenMeasurementSystemsResponseDto
        {
            Id = oxygenMeasurementSystem.Id,
            SystemName = oxygenMeasurementSystem.SystemName,
            Zipcode = oxygenMeasurementSystem.Zipcode,
            Location = oxygenMeasurementSystem.Location

        };
    }

    public static List<OxygenMeasurementSystemsResponseDto?> ToResponseList(
        this IEnumerable<OxygenMeasurementSystem> dbSystems)
    {
        return dbSystems.Select(oms => oms.ToSimpleResponse()).ToList();
    }
}