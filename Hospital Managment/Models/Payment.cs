using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId ")]
        public Patient Patient { get; set; }
        public int BillId { get; set; }
        [ForeignKey("BillId ")]
        public Bill Bill { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
