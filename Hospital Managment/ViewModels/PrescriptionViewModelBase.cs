using Hospital_Managment.Models;
using Microsoft.AspNetCore.Mvc.Rendering.Schema;

namespace Hospital_Managment.ViewModels
{
    public class PrescriptionViewModelBase
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int AppoitmentId { get; set; }

        public SelectListItem Doctor { get; set; }
        public SelectListItem Patient { get; set; }
        public SelectListItem Appoitment { get; set; }

        public DataTime datetime { get; set; }
        public string notes { get; set; }
    }
}