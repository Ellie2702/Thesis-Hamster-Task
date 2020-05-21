using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class EmployeeCode
    {
        [Key]
        public int CodeID { get; set; }
        [Required]
        [StringLength(6)]
        public string Code { get; set; }
        [Required]
        public bool isUsed { get; set; }
        public Company Company { get; set; }
    }
}
