using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class MessageDTO
    {
        public int MessgeID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}
