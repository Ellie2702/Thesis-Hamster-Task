using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class DocumentDTO
    {
        public int DocumentID { get; set; }
        public string DocType { get; set; }
        public int UserID { get; set; }
    }
}
