using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.Entities
{
    public class User
    {
        public User()
        {
            Images = new HashSet<Image>();
        }
        [Key]
        public int UserID { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string SecondName { get; set; }
        [Required]
        [MaxLength(8)]
        public string  Salt { get; set; }
        [Required]
        [MaxLength(100)]
        public string PassHash { get; set; }
        [Required]
        [StringLength(50)]
        public string Login { get; set; }
        public DateTime Birth { get; set; }
        [Required]
        public DateTime RegDate { get; set; }
        [Required]
        public int RoleID { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        [Required]
        public string Email { get; set; }


        public Role Role { get; set; }

        public virtual ICollection<Image> Images { get; set; }
 

    }
}
