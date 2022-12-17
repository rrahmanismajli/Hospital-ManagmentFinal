using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<Prescription> Prescription { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Treatment> Treatments { get; set; }
        public List<DoctorAppointment> DoctorAppointments{ get; set; }
        public List<Hospitalization> Hospitalizations{ get; set; }
    }

}
