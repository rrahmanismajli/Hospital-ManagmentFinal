using Hospital_Managment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.ViewModels
{
    public class BillViewModel
    {
        public int Id { get; set; }

        public int? PatientId { get; set; }
        public List<Patient> Patient { get; set; }
        public int? AppointmentId { get; set; }
        public List<Appointment> Appointment { get; set; }
        public int? PaymenttId { get; set; }
        public List<Payment>Payment { get; set; }

        public double Amount { get; set; }
        public string Notes { get; set; }

        public bool Paid { get; set; }
    }
}
