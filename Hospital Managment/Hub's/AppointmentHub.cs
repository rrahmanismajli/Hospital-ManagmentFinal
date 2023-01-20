using Microsoft.AspNetCore.SignalR;

namespace Hospital_Managment.Hub_s
{
    public class AppointmentHub : Hub 
    {
        public async Task SendAppointmentNotification(string doctorId, string patientName, string userId)
        {
            // Send the notification to the specific doctor
            await Clients.User(doctorId).SendAsync("ReceiveAppointmentNotification", patientName, userId);
        }

    }
    
}
