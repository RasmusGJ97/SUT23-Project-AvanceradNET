using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_Project_AvanceradNET.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "FirstName is required . . .")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "First Name should be between 3 and 16 characters")]
        public string CustomerFName { get; set; }
        [Required(ErrorMessage = "FirstName is required . . .")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Last Name should be between 2 and 30 characters")]
        public string CustomerLName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public ICollection<Appointment> Appointment { get; set; }
    }
}
