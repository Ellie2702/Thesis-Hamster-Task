using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [StringLength(20)]
        [Required]
        public string RoleName { get; set; }
        
        
    }
}
