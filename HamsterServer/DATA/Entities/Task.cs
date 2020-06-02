using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Task
    {
        [Key]
        public int TaskID { get; set; }
        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        [Required]
        [StringLength(600)]
        public string Descript { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime Deadline { get; set; }
        public bool isDone { get; set; }

        public User User { get; set; }
    }
}
