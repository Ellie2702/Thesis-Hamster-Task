﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class TaskExecutors
    {
        [Key]
        public int TaskExecutorsID { get; set; }
        [Required]
        public int TaskID { get; set; }
        [Required]
        public int UserID { get; set; }

        public Task Task { get; set; }
        public User User { get; set; }
    }
}
