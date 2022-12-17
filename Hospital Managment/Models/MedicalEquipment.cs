using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class MedicalEquipment
    {
        [Key]
        public int MedicalEquipmentId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
