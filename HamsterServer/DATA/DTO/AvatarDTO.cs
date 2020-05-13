using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class AvatarDTO
    {
        public int AvatarID { get; set; }
        public int ImageID { get; set; }
        public int UserID { get; set; }
        public bool IsUsed { get; set; }
    }
}
