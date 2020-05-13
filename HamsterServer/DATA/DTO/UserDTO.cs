using HamsterServer.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public byte[] Salt { get; set; }
        public byte[] PassHash { get; set; }
        public string Login { get; set; }
        public DateTime Birth { get; set; }
        public DateTime RegDate { get; set; }
        public int RoleID { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }


        public List<Avatar> Avatars { get; set; }
    }
}
