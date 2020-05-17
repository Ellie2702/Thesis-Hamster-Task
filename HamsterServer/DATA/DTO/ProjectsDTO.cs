using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class ProjectsDTO
    {
        public int ProjectID { get; set; }
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public string Title { get; set; }
        public string Descript { get; set; }
    }
}
