using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class CompanyLogo
    {
        [Key]
        public int CompanyLogoID { get; set; }
        [Required]
        public bool isUsed { get; set; }
        [Required]
        public Image Image { get; set; }
        public Company Company { get; set; }
      

    }
}
