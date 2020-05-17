using HamsterServer.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.Global
{
    public class Auth
    {
        public Auth(Guid guid, User user)
        {
            this.guid = guid;
            this.user = user;
        }

        public Guid guid;
        public User user;

    }
}
