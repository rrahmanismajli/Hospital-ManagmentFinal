using Hospital_Managment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.ViewModels
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
     
        public int DoctorId { get; set; }
     
        public SelectListItem Doctor { get; set; }
        public DateTime DateTime { get; set; }
        public int NurseId { get; set; }

        public SelectListItem Nurse { get; set; }

        public Bill Bill { get; set; }
        public int ReceptionistId { get; set; }
        public SelectListItem Receptionist { get; set; }
        public string ReasonForVisit { get; set; }
        public string Notes { get; set; }
    }
}
