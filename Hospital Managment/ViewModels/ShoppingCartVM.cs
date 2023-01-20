using Hospital_Managment.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ListCart{ get; set; }
        [NotMapped]

        public OrderHeader OrderHeader { get; set; }
    }
}
