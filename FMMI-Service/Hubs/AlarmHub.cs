using Microsoft.AspNetCore.SignalR;

namespace FMMI_Service.Hubs;

public class AlarmHub : Hub
{
    public async Task NewAlarm()
    {
        await Clients.All.SendAsync("NewAlarm");
    }
}
