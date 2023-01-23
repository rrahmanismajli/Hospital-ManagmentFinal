using Hospital_Managment.Models;

namespace Hospital_Managment.ViewModels
{
    public class OrderVM
    {
        public OrderHeader  OrderHeader{ get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
