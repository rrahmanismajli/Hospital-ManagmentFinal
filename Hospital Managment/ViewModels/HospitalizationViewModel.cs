using Hospital_Managment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.ViewModels
{
    public class HospitalizationViewModel
    {
        public int HospitalizationId { get; set; }
        public int? PatientId { get; set; }
        public List<Patient> Patient { get; set; }
        public int? DoctorId { get; set; }
        public List<Doctor> Doctor { get; set; }

        public string RoomNumber { get; set; }
       
        public DateTime StartDate { get; set; }
       
        public DateTime EndDate { get; set; }
    }
}
