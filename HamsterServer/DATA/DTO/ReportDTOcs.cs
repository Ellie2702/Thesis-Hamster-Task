using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class ReportDTOcs
    {
        public int ReportID { get; set; }
        public int UserID { get; set; }
        public int DocumentID { get; set; }
        public DateTime Date { get; set; }
    }
}
