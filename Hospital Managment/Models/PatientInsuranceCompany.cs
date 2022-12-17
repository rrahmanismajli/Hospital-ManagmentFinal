using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.Models
{
    public class PatientInsuranceCompany
    {

        public int PatientId { get; set; }
       
        public Patient Patient { get; set; }
        public int InsuranceCompanyId { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }
    }

}
