using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class Receptionist
    {
        [Key]
        public int ReceptionistId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}
