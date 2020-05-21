﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Image
    {
        [Key]
        public int ImageID { get; set; }
        [Required]
        [StringLength(7)]
        public byte[] ImageBytes { get; set; }
        public string ImgType { get; set; }

        public User User { get; set; }
    }
}
