using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Position
    {
        [Key] 
        public int PositionID { get; set; }
        [StringLength(50)]
        [Required]
        public string PositionName { get; set; }
       

       
    
    }
}
