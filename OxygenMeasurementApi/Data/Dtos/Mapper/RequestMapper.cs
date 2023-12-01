using OxygenMeasurementApi.Authorization;
using OxygenMeasurementApi.Data.Dtos.RequestDtos;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Dtos.Mapper;

public static class RequestMapper
{
    public static OxygenMeasurementSystem ToEntity(this CreateOxygenMeasurementSystemDto systemDto)
    {
        return new OxygenMeasurementSystem
        {
            SystemName = systemDto.SystemName,
            Zipcode = systemDto.Zipcode,
            Location = systemDto.Location,
            AdminstratorEmail = systemDto.AdminstratorEmail,
            ApiKey = new ApiKey
            {
                ApiKeyId = ApiKeyGenerator.GenerateNewApiKey(15)
            }
        };
    }
}