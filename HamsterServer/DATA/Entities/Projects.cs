using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Projects
    {
        [Key]
        public int ProjectID { get; set; }
        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        [Required]
        [StringLength(300)]
        public string Descript { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        public Company Company { get; set; }
        [Required]
        public User User { get; set; }

    }
}
