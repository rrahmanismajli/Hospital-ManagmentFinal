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
        [ForeignKey("AppointmentId")]
        [Required(ErrorMessage = "Medicine Name is required")]
        public string MedicineName { get; set; }
        [Required(ErrorMessage = "Dosage is required")]
        public string Dosage { get; set; }
        [Required(ErrorMessage = "Frequency is required")]
        public string Frequency { get; set; }
        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }
        [Required(ErrorMessage = "Notes is required")]
        public string Notes { get; set; }

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }
    }

}
