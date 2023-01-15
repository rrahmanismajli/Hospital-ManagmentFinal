using Hospital_Managment.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.ViewModels
{
    public class DoctorViewModel
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        public int DepartmentId { get; set; }

        [ValidateNever]
        public SelectListItem Department { get; set; }
        [ValidateNever]
        public int AppointmentCount { get; set; }
    }
}
