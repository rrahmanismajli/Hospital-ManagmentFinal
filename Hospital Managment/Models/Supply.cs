using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class Supply
    {
        [Key]
        public int SupplyId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
