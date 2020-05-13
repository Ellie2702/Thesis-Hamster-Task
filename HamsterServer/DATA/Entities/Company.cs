using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class Company
    {
        public Company()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int CompanyID { get; set; }
        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }
        [StringLength(50)]
        public string CompanyType { get; set; }
        public DateTime FoundationDate { get; set; }
        [Required]
        public DateTime RegDate { get; set; }

        
        public virtual ICollection<Employee> Employees { get; set; }
        
    }
}
