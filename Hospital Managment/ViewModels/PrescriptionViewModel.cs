using Hospital_Managment.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;

namespace Hospital_Managment.ViewModels
{
    public class PrescriptionViewModel
    {
        public List<Patient> Patient { get; set; }
        public List<Doctor> Doctor { get; set; }
        public List<Appointment> Appointment { get; set; } 
        public string? MedicineName { get; set; }
        public string? Dosage { get; set; }
        public string? Frekuency { get; set; }
         public int? Duration { get; set; }

        public string Summary { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId ")]

        public int? DoctorId { get; set; }
        public int? AppointmentId { get; set; }
    }

}
