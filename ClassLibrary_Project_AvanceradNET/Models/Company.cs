using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_Project_AvanceradNET.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "Name is required . . .")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 30 characters")]
        public string CompanyName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        public string CompanyPassword { get; set; }
    }
}
