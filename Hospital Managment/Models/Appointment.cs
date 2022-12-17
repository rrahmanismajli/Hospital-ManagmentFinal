
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId ")]
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId ")]
        public Doctor Doctor { get; set; }
        public DateTime DateTime { get; set; }
        public int NurseId { get; set; }
 
        public Nurse Nurse { get; set; }
       
        public Bill Bill { get; set; }
        public int ReceptionistId { get; set; }
        public Receptionist Receptionist { get; set; }
        public string ReasonForVisit { get; set; }
        public string Notes { get; set; }

    
        
     
        public List<Prescription> Prescriptions { get; set; }
        public List<TestResult> TestResults { get; set; }
        public List<DoctorAppointment> DoctorAppointments{ get; set; }
    }

}
