using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class MedicalEquipment
    {
        [Key]
        public int MedicalEquipmentId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
