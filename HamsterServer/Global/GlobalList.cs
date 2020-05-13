using HamsterServer.DATA;
using HamsterServer.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.Global
{
    public class GlobalList
    {
        public static List<Auth> Authorized = new List<Auth>();

        public static User GetUser(string login, string pass)
        {
            using(DataContext db = new DataContext())
            {
                User user = db.Users.Where(p => p.Login == login).FirstOrDefault();
                byte[] hash = DATA.BLL.Cipher.PassHash(pass, user.Salt);
                if(user.PassHash == hash.ToString())
                {
                    return user;
                }
            }
           
            return null;
        }

        public static Auth IsAuthed(Guid guid)
        {
            foreach (Auth auth in Authorized)
            {
                if (auth.guid == guid) return auth;
            }

            return null;
        }
    }
}
