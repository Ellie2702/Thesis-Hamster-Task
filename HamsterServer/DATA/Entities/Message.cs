using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Message
    {
        
        [Key]
        public int MessageID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [MaxLength(600)]
        public string Content { get; set; }
        
        public User UserTo_UserId { get; set; }
   
        public User UserFrom_UserId { get; set; }
        [Required]
        public DateTime TimeSend { get; set; }
        public bool isCheck { get; set; }
    }
}
