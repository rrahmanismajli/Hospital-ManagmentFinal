using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hospital_Managment.ViewModels
{
    public class ProfileViewModel
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string fullAdress { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
