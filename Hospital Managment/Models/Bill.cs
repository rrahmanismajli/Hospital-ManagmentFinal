using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public double Amount { get; set; }
        public string Notes { get; set; }
        public bool Paid { get; set; }


        public List<Payment> Payments { get; set; }
        public Appointment Appointment { get; set; }
    }

}
