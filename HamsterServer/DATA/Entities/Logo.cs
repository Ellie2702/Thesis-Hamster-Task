using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Logo
    {
        [Key]
        public int LogoID { get; set; }
        public Image Image { get; set; }
     
    }
}
