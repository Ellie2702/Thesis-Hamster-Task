using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class SсheldueDTO
    {
        public int SсheldueID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Comment { get; set; }
        public int EmployeeID { get; set; }
    }
}
