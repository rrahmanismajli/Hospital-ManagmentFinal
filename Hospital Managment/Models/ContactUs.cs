using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class ContactUs
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(13, MinimumLength = 9, ErrorMessage = "Phone Number must be between 9 and 13 numbers")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
