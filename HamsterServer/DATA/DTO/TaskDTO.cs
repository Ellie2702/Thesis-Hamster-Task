using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class TaskDTO
    {
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Descript { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Deadline { get; set; }
    }
}
