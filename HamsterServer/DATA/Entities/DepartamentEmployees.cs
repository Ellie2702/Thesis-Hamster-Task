using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class DepartamentEmployees
    {
        [Key]
        public int DepartamentEmployeeID { get; set; }
        public Department Department { get; set; }
        public Employee Employee { get; set; }
    }
}
