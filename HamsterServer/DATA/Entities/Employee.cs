using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public Company Company { get; set; }
       
        public Position Position { get; set; }
        public ICollection<DepartamentEmployees> DepartamentEmployees { get; set; }
    }
}
