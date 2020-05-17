using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Message
    {
        [Key]
        public int MessgeID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [MaxLength(600)]
        public string Content { get; set; }
        [Required]
        public int From { get; set; }
        [Required]
        public int To { get; set; }

        public User User { get; set; }
    }
}
