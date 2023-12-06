using OxygenMeasurementApi.Authorization;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Mappers;

/// <summary>
/// Mapper class for converting request DTOs to EntityFramework entities.
/// </summary>
public static class RequestToDbEntityMapper
{
    /// <summary>
    /// Converts an AddOxygenMeasurementSystemRequestDto to an OxygenMeasurementSystem entity.
    /// </summary>
    /// <param name="systemRequestDto">The AddOxygenMeasurementSystemRequestDto to convert.</param>
    /// <returns>The mapped OxygenMeasurementSystem entity with a newly generated API key.</returns>
    public static OxygenMeasurementSystem ToDbEntity(this AddOxygenMeasurementSystemRequestDto systemRequestDto)
    {
        // Use ApiKeyGenerator.GenerateNewApiKey to generate a new API key with a length of 15 characters.
        return new OxygenMeasurementSystem
        {
            SystemName = systemRequestDto.SystemName,
            Location = systemRequestDto.Location,
            SystemNotificationAdvisors = systemRequestDto.SystemNotificationAdvisors.ToDbEntityList(),
            ApiKey = new ApiKey
            {
                ApiKeyValue = ApiKeyGenerator.GenerateNewApiKey(15)
            }
        };
    }
    /// <summary>
    /// Converts a list of AddSystemNotificationAdvisorRequestDto to a list of SystemNotificationAdvisor entities.
    /// </summary>
    /// <param name="systemAdvisors">The list of AddSystemNotificationAdvisorRequestDto to convert.</param>
    /// <returns>The mapped list of SystemNotificationAdvisor entities.</returns>
    public static List<SystemNotificationAdvisor> ToDbEntityList(this IEnumerable<AddSystemNotificationAdvisorRequestDto> systemAdvisors)
    {
        return systemAdvisors.Select(systemAdvisorDto => systemAdvisorDto.ToDbEntity()).ToList();
    }
    /// <summary>
    /// Converts an AddSystemNotificationAdvisorRequestDto to a SystemNotificationAdvisor entity.
    /// </summary>
    /// <param name="addSystemNotificationAdvisorRequestDto">The AddSystemNotificationAdvisorRequestDto to convert.</param>
    /// <returns>The mapped SystemNotificationAdvisor entity.</returns>
    public static SystemNotificationAdvisor ToDbEntity(this AddSystemNotificationAdvisorRequestDto addSystemNotificationAdvisorRequestDto)
    {
        return new SystemNotificationAdvisor
        {
            Email = addSystemNotificationAdvisorRequestDto.Email
        };
    }
    /// <summary>
    /// Converts an AddOxygenMeasurementRequestDto to an OxygenMeasurement entity.
    /// </summary>
    /// <param name="requestDto">The AddOxygenMeasurementRequestDto to convert.</param>
    /// <returns>The mapped OxygenMeasurement entity.</returns>
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