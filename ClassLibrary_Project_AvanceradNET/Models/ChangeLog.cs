using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_Project_AvanceradNET.Models
{
    public class ChangeLog
    {
        [Key]
        public int ChangeId { get; set; }
        [Required]
        public DateTime OldStart { get; set; }
        [Required]
        public DateTime NewStart { get; set; }
        [Required]
        public DateTime OldEnd { get; set; }
        [Required]
        public DateTime NewEnd { get; set; }
        [Required]
        public DateTime ChangedDate { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
