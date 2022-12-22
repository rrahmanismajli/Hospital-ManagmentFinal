using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class Supply
    {
        [Key]
        public int SupplyId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
