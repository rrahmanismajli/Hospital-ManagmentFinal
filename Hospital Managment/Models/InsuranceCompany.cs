using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class InsuranceCompany
    {
        [Key]
        public int InsuranceCompanyId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<PatientInsuranceCompany> PatientInsuranceCompanies { get; set; }
        public List<Patient> Patients { get; set; }

    }
}
