using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Document
    {
        [Key]
        public int DocumentID { get; set; }
        [Required]
        [StringLength(7)]
        public string DocType { get; set; }
        [Required]
        public int UserID { get; set; }

        public User User { get; set; }
    }
}
