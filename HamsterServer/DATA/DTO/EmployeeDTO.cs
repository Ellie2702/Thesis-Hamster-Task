using HamsterServer.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class EmployeeDTO
    {
        public int CompanyEmployeeID { get; set; }
        public Company CompanyID { get; set; }
        public User UserID { get; set; }
        public Position PositionID { get; set; }


    }
}
