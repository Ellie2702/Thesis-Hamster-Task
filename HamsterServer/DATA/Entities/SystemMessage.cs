using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class SystemMessage
    {
        [Key]
        public int SystemMessageID { get; set; }
        [Required]
        [StringLength(300)]
        public string MessageType { get; set; }
        [Required]
        public int UserID { get; set; }

        public User User { get; set; }
    }
}
