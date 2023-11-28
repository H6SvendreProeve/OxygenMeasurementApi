using Microsoft.AspNetCore.SignalR;
using OxygenMeasurementApi.Data.Entities;

namespace OxygenMeasurementApi;

public sealed class OxygenMeasurementHub : Hub
{
    public async Task SendOxygenMeasurement(OxygenMeasurement oxygenMeasurement)
    {
        await Clients.All.SendAsync("ReceiveOxygenMeasurement", oxygenMeasurement);
    }
}