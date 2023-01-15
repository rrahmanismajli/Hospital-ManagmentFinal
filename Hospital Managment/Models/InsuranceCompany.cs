using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class InsuranceCompany
    {
        [Key]
        public int InsuranceCompanyId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(13, MinimumLength = 9, ErrorMessage = "Phone Number must be between 9 and 13 numbers")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [ValidateNever]

        public List<PatientInsuranceCompany> PatientInsuranceCompanies { get; set; }
        [ValidateNever]
        public List<Patient> Patients { get; set; }

    }
}
