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
        public string TestName { get; set; }
        public string Result { get; set; }
        public string Notes { get; set; }

        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }
    }

}
