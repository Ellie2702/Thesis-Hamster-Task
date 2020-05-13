using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Employee
    {
        [Key]
        public int CompanyEmployeeID { get; set; }
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int PositionID { get; set; }

       public User User { get; set; }
        public Company Company { get; set; }
        public Position Position { get; set; }
    }
}
