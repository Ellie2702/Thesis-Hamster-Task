using HamsterServer.DATA;
using HamsterServer.DATA.Entities;
using HamsterServer.Global;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace HamsterServer
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().ServerListener(new string[] { "http://localhost:8080/" });
        }

       

        private void ServerListener(string[] pref)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            if (pref == null || pref.Length == 0)
                throw new ArgumentException("prefixes");
            HttpListener Listener = new HttpListener();

            foreach (string s in pref)
            {
                Listener.Prefixes.Add(s);
            }

            Listener.Start();
            Console.WriteLine("Listening...");
            while (true)
            {
                HttpListenerContext context = Listener.GetContext();
                HttpListenerRequest request = context.Request;

                Console.WriteLine("Got request from: {0}", request.Url);


                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                string responseString = GetResult(request.Url.LocalPath);
                //string responseString = "Hello world!";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
        }


        private string GetResult(string guid)
        {
            string[] part = guid.Split('/');

            if (part.Length > 1)
            {
                switch (part[1])
                {
                    case "auth":
                        if (part.Length != 4) return "need 2 arguments";
                        return TryAuth(part[2], part[3]);
                    default:
                        return "404";
                }
            }
            else return "need more arguments!";
        }

        private string TryAuth(string login, string password)
        {
            using (DataContext db = new DataContext())
            {
                User user = db.Users.Where(User => User.Login == login).FirstOrDefault();
                if(user.PassHash == DATA.BLL.Cipher.PassHash(password, user.Salt).ToString())
                {
                    var guid = Guid.NewGuid();
                    Auth auth = new Auth(guid, user);
                    Global.GlobalList.Authorized.Add(auth);
                    return "ok";
                }
                return "bad";
            }
        }
    }
}
