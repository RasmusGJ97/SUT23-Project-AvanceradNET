using System.ComponentModel.DataAnnotations;



namespace ClassLibrary_Project_AvanceradNET.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        [Required]
        public DateTime AppointmentStart { get; set; }
        [Required]
        public DateTime AppointmentEnd { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ChangeLog>? ChangeLogs { get; set; }


    }
}
