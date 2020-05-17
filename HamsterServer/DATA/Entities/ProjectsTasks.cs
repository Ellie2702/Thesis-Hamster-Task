using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class ProjectsTasks
    {
        [Key]
        public int ProjectsTasksID { get; set; }
        [Required]
        public int ProjectID { get; set; }
        [Required]
        public int TaskID { get; set; }

        public Projects Projects { get; set; }
        public Task Task { get; set; }

    }
}
