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
        [Required]
        public bool Report { get; set; }
        [Required]
        public bool Tasks { get; set; }
        [Required]
        public bool Departament { get; set; }
        [Required]
        public bool Projects { get; set; }
        [Required]
        public bool EmpCode { get; set; }
        [Required]
        public bool Schedule { get; set; }
        [Required]
        public bool Marks { get; set; }
    }
}
