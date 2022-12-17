using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string InsuranceProvider { get; set; }
        public string PrimaryCarePhysician { get; set; }

  
        public List<Appointment> Appointments { get; set; }
        public List<Bill> Bills { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public List<TestResult> TestResults { get; set; }
        public List<InsuranceCompany> InsuranceCompanies{ get; set; }
        public List<Treatment> Treatments{ get; set; }
        public List<Hospitalization> Hospitalizations { get; set; }
        public List<PatientInsuranceCompany> PatientInsuranceCompanies{ get; set; }
    }

}
