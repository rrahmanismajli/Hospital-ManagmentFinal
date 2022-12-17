using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.Models
{
    public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId ")]
        public Patient Patient { get; set; }
        
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId ")]
        public Doctor Doctor { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
