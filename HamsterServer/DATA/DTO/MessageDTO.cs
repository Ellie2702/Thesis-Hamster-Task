using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class MessageDTO
    {
        public int MessageID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserTo { get; set; }
        public int UserFrom { get; set; }
        public DateTime TimeSend { get; set; }
        public bool isCheck { get; set; }
    }
}
