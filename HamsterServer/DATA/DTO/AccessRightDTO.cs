using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class AccessRightDTO
    {
        public int AccessRightID { get; set; }
        public int EmployeeID { get; set; }
        public bool Report { get; set; }
        public bool Tasks { get; set; }
        public bool Departament { get; set; }
        public bool Projects { get; set; }
        public bool EmpCode { get; set; }
        public bool Graphick { get; set; }
        public bool Marks { get; set; }
    }
}
