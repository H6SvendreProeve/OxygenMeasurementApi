using Microsoft.AspNetCore.SignalR;
using OxygenMeasurementApi.Entities;

namespace OxygenMeasurementApi;

public sealed class OxygenMeasurementHub : Hub
{
    public async Task SendOxygenMeasurement(OxygenMeasurement oxygenMeasurement)
    {
        await Clients.All.SendAsync("ReceiveOxygenMeasurement", oxygenMeasurement);
    }
    
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
    }
}