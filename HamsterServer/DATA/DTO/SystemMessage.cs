﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class SystemMessage
    {
        public int SystemMessageID { get; set; }
        public string MessageType { get; set; }
        public int UserID { get; set; }
    }
}