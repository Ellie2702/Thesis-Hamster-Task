using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class TaskFile
    {
        [Key]
        public int TaskFileID { get; set; }
        [Required]
        public Document File { get; set; }
        [Required]
        public Task Task { get; set; }
    }
}
