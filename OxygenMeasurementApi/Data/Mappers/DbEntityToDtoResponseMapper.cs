using OxygenMeasurementApi.Data.Dtos.ResponseDtos;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Mappers;

/// <summary>
/// Mapper class for converting EntityFramework entities to response DTOs.
/// </summary>
public static class DbEntityToDtoResponseMapper
{
    /// <summary>
    /// Converts an OxygenMeasurementSystem entity to an OxygenMeasurementSystemResponseDto.
    /// </summary>
    /// <param name="oxygenMeasurementSystem">The OxygenMeasurementSystem entity to convert.</param>
    /// <returns>The mapped OxygenMeasurementSystemResponseDto.</returns>
    public static OxygenMeasurementSystemResponseDto? ToResponse(this OxygenMeasurementSystem oxygenMeasurementSystem)
    {
        return new OxygenMeasurementSystemResponseDto
        {
            Id = oxygenMeasurementSystem.OxygenMeasurementSystemId,
            SystemName = oxygenMeasurementSystem.SystemName,
            Location = oxygenMeasurementSystem.Location,
            ApiKeyValue = oxygenMeasurementSystem.ApiKey.ApiKeyValue,
            SystemNotificationAdvisors = oxygenMeasurementSystem.SystemNotificationAdvisors.ToResponseList()
        };
    }

    /// <summary>
    /// Converts a SystemNotificationAdvisor entity to an SystemNotificationAdvisorResponseDto.
    /// </summary>
    /// <param name="systemNotificationAdvisor">The SystemNotificationAdvisor entity to convert.</param>
    /// <returns>The mapped SystemNotificationAdvisorResponseDto.</returns>
    public static SystemNotificationAdvisorResponseDto ToResponse(this SystemNotificationAdvisor systemNotificationAdvisor)
    {
        return new SystemNotificationAdvisorResponseDto
        {
            SystemNotificationAdvisorId = systemNotificationAdvisor.SystemNotificationAdvisorId,
            Email = systemNotificationAdvisor.Email
        };
    }

    /// <summary>
    /// Converts a list of SystemNotificationAdvisor entities to a list of SystemNotificationAdvisorResponseDto.
    /// </summary>
    /// <param name="systemNotificationAdvisors">The list of SystemNotificationAdvisor entities to convert.</param>
    /// <returns>The mapped list of SystemNotificationAdvisorResponseDto.</returns>
    public static List<SystemNotificationAdvisorResponseDto> ToResponseList(this IEnumerable<SystemNotificationAdvisor> systemNotificationAdvisors)
    {
        return systemNotificationAdvisors.Select(systemNotificationAdvisor => systemNotificationAdvisor.ToResponse()).ToList();
    }
    

    /// <summary>
    /// Converts an OxygenMeasurement entity to an OxygenMeasurementResponseDto.
    /// </summary>
    /// <param name="oxygenMeasurement">The OxygenMeasurement entity to convert.</param>
    /// <returns>The mapped OxygenMeasurementResponseDto.</returns>
    public static OxygenMeasurementResponseDto? ToResponse(this OxygenMeasurement oxygenMeasurement)
    {
        return new OxygenMeasurementResponseDto
        {
            Id = oxygenMeasurement.OxygenMeasurementId,
            OxygenValue = oxygenMeasurement.OxygenValue,
            MeasurementTime = oxygenMeasurement.MeasurementTime,
            OxygenMeasurementSystemId = oxygenMeasurement.OxygenMeasurementSystemId,
            SystemName = oxygenMeasurement.OxygenMeasurementSystem.SystemName,
            SystemLocation = oxygenMeasurement.OxygenMeasurementSystem.Location
        };
    }
}
