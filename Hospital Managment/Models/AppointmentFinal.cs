using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.Models
{
    public class AppointmentFinal
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        public string ReasonForVisit { get; set; }
        public string Notes { get; set; }
        [ValidateNever]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId ")]
        [ValidateNever]
        public Doctor Doctor { get; set; }
       
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }


    }
}
