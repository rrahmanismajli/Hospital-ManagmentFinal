using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class Nurse
    {
        [Key]
        public int NurseId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(13, MinimumLength = 9, ErrorMessage = "Phone Number must be between 9 and 13 numbers")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [ValidateNever]
        public List<Appointment> Appointments { get; set; }
        [ValidateNever]
        public string  ImageUrl { get; set; }
    }
}
