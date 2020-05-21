using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; }
  
        [Required]
        public DateTime Date { get; set; }

        public User User { get; set; }
        public Document Document { get; set; }
    }
}
