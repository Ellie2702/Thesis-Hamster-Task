using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
            EmployeeCodes = new HashSet<EmployeeCode>();
            Departments = new HashSet<Department>();
        }

        [Key]
        public int CompanyID { get; set; }
        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }
        [StringLength(50)]
        public string CompanyType { get; set; }
        [Required]
        public DateTime FoundationDate { get; set; }
        [Required]
        public DateTime RegDate { get; set; }
        public User User { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<EmployeeCode> EmployeeCodes { get; set; }
    }
}
