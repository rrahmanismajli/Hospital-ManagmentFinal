

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Managment.Models
{
    public class PharmacyProduct
    {
        [Key]
        public int productId { get; set; }
        [DisplayName("Product Name")]
        public string productName { get; set; }
        [DisplayName("Expiration Date")]
        public DateTime BestBeforeDate { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }
        [DisplayName("Price")]
        public decimal price { get; set; }
        [DisplayName("Description")]
        public string description { get; set; }
        [DisplayName("Quantity")]
        public int quantity { get; set; }
        public string shipped { get; set; }

        public string OriginFlagUrl { get; set; }

    }
}
