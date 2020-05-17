using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class EmployeeCodeDTO
    {
        public int CodeID { get; set; }
        public string Code { get; set; }
        public int CompanyID { get; set; }
        public bool isUsed { get; set; }
    }
}
