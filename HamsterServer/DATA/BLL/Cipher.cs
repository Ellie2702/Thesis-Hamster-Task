using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA.BLL
{
    public class Cipher
    {

        public static byte[] NewSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[8];
            rng.GetBytes(salt);

            return salt;
        }

        public static byte[] PassHash(string pass, string salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            var temp = Encoding.UTF8.GetBytes(pass + salt);
            byte[] res = new byte[temp.Length + salt.Length];

            int i = 0;
            foreach(var b in temp)
            {
                res[i++] = b;
            }

           // foreach (var b in salt)
          //  {
         //       res[i++] = b;
          //  }

            return algorithm.ComputeHash(res);
        }
    }
}
