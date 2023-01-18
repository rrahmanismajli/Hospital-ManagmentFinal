using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(13, MinimumLength = 9, ErrorMessage = "Phone Number must be between 9 and 13 numbers")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
         public string Email { get; set; }
        [Required(ErrorMessage = "Adress is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Insurance Provider is required")]
        public string InsuranceProvider { get; set; }
        [Required(ErrorMessage = "Primary Care Physician  is required")]
        public string PrimaryCarePhysician { get; set; }

        [ValidateNever]
        public List<Appointment> Appointments { get; set; }
        [ValidateNever]
        public List<Payment> Payment { get; set; }

        [ValidateNever]
        public List<Bill> Bills { get; set; }
        [ValidateNever]
        public List<Prescription> Prescriptions { get; set; }
        [ValidateNever]
        public List<TestResult> TestResults { get; set; }
        [ValidateNever]
        public List<InsuranceCompany> InsuranceCompanies{ get; set; }
        [ValidateNever]
        public List<Treatment> Treatments{ get; set; }
        [ValidateNever]
        public List<Hospitalization> Hospitalizations { get; set; }
        [ValidateNever]
        public List<PatientInsuranceCompany> PatientInsuranceCompanies{ get; set; }
    }

}
