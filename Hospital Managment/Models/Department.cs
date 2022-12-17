using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

       
    }
}
