using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Image Logo { get; set; }
        [Required]
        public Company Company { get; set; }
      

    }
}
