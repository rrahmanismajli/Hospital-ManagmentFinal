using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId ")]
        public Patient Patient { get; set; }
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public double Amount { get; set; }
        [Required(ErrorMessage = "Notes is required")]
        public string Notes { get; set; }
        [Required(ErrorMessage = "Paid is required")]
        public bool Paid { get; set; }


        public List<Payment> Payments { get; set; }
        public Appointment Appointment { get; set; }
    }

}
