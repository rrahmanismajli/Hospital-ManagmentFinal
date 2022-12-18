using Hospital_Managment.Models;

namespace Hospital_Managment.ViewModels
{
    public class PacientAppoitmentDoctor
    {
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public String DepartamentName { get; set; }
    }
}
