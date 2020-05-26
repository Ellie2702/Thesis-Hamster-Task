using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Department
    {
        [Key]
        public int DepartamentID { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public Company Company { get; set; }
    }
}
