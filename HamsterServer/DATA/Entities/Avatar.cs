using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Avatar
    {
        [Key]
        public int AvatarID { get; set; }
        [Required]
        public int ImageID { get; set; }
        [Required]
        public bool IsUsed { get; set; }

        public User Owner { get; set; }
        public Image Image { get; set; }
    }
}
