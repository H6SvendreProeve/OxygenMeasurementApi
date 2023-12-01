using OxygenMeasurementApi.Data.Dtos.ResponseDtos;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi.Data.Dtos.Mapper;

public static class ResponseMapper
{
    public static OxygenMeasurementSystemResponseDto ToResponse(this OxygenMeasurementSystem oxygenMeasurementSystem)
    {
        return new OxygenMeasurementSystemResponseDto
        {
            Id = oxygenMeasurementSystem.Id,
            SystemName = oxygenMeasurementSystem.SystemName,
            Zipcode = oxygenMeasurementSystem.Zipcode,
            Location = oxygenMeasurementSystem.Location,
            AdminstratorEmail = oxygenMeasurementSystem.AdminstratorEmail,
            ApiKeyId = oxygenMeasurementSystem.ApiKeyId,

        };
    }
}