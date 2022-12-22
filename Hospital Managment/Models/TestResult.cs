using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.Models
{
    public class TestResult
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId ")]
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId ")]
        [Required(ErrorMessage = "Test Name is required")]
        public string TestName { get; set; }
        [Required(ErrorMessage = "Result is required")]
        public string Result { get; set; }
        [Required(ErrorMessage = "Notes is required")]
        public string Notes { get; set; }

        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }
    }

}
