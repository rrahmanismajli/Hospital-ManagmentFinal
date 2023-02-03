using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Specialty is required")]
        public string Specialty { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(13, MinimumLength = 9, ErrorMessage = "Phone Number must be between 9 and 13 numbers")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Adress is required")]
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        [ValidateNever]
        public Department Department { get; set; }
       
        [ValidateNever]
        public string  ImageUrl { get; set; }
        public bool? isVisible { get; set; }
        [ValidateNever]
        public List<Prescription> Prescription { get; set; }
        [ValidateNever]
        public List<Appointment> Appointments { get; set; }
        [ValidateNever]
        public List<Treatment> Treatments { get; set; }
        [ValidateNever]
        public List<DoctorAppointment> DoctorAppointments{ get; set; }
        [ValidateNever]
        public List<Hospitalization> Hospitalizations{ get; set; }
    }

    


}
