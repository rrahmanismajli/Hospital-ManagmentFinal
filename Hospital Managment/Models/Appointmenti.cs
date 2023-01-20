using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Managment.Models
{
    public class Appointmenti
    {
        [Key]
        public int Id { get; set; }
        [ValidateNever]
        public string PatientName { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public virtual ApplicationUser User { get; set; }
        [ValidateNever] 
        public List<Doctor> Doctors { get; set; }
    }
}
