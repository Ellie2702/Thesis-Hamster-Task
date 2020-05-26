using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class DepartamentEmployees
    {
        [Key]
        public int DepartamentEmployeeID { get; set; }
        [Required]
        public Department Department { get; set; }
        [Required]
        public Employee Employee { get; set; }
    }
}
