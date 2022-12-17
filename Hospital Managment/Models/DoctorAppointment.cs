using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.Models
{
    public class DoctorAppointment
    {
        public int DoctorId { get; set; }
     
        public Doctor Doctor { get; set; }
        public int AppointmentId { get; set; }
       
        public Appointment Appointment { get; set; }
    }
}
