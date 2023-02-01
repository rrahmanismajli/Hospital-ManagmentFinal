using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.Models
{
    public class Appointments
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }
        public DateTime DateTimeOfAppointment { get; set; }
        public string email { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(13, MinimumLength = 9, ErrorMessage = "Phone Number must be between 9 and 13 numbers")]
        public string PhoneNumber { get; set; }
        public string fullAdress { get; set; }

        public string ReasonOfVisiting { get; set; }

        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        [ValidateNever]
        public Doctor Doctor { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

    }
}