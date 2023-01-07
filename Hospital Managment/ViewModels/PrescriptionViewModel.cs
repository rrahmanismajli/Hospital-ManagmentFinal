using Hospital_Managment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace Hospital_Managment.ViewModels
{
    public class PrescriptionViewModel
    {
        public int DoctorId { get; set; }
        public SelectListItem Doctor { get; set; }
        public int PatientId { get; set; }
        public SelectListItem Patient { get; set; }
        public int AppointmentId { get; set; }
        public SelectListItem Appointment { get; set; }
        public string MedicineName{ get; set; }
        public string Dosage { get; set; }

        public string Frequency { get; set; }
        public int Duration { get; set; }

        public string Notes { get; set; }





    }
}