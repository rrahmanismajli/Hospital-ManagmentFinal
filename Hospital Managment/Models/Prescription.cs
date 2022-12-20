using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId ")]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId ")]
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId ")]
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public int Duration { get; set; }
        public string Notes { get; set; }

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }
    }

}
