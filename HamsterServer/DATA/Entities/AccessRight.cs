using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class AccessRight
    {
        [Key]
        public int AccessRightID { get; set; }
        [Required]
        public Employee Employee { get; set; }
        public bool Tasks { get; set; }
        public bool Departament { get; set; }
        public bool Projects { get; set; }
        public bool EmpCode { get; set; }
        public bool Schedule { get; set; }
    }
}
