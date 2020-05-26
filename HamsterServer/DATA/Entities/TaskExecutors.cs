using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class TaskExecutors
    {
        [Key]
        public int TaskExecutorsID { get; set; }
        [Required]
        public Task Task { get; set; }
        [Required]
        public User User { get; set; }
    }
}
