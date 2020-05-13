using HamsterServer.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class CompanyDTO
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public DateTime FoundationDate { get; set; }
        public DateTime RegDate { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
