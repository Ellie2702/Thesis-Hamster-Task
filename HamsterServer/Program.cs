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
                        if (part.Length != 4) return part.Length.ToString();
                        return TryAuth(part[2], part[3]);
                    case "reguserA":
                        if (part.Length != 8) return part.Length.ToString();
                        return TryRegistration("A", part);
                    case "reguserFull":
                        if (part.Length != 9) return part.Length.ToString();
                        return TryRegistration("Full", part);
                    case "RegOrg":
                        if (part.Length != 6) return part.Length.ToString();
                        return TryRegistrationOrg(part);
                    default:
                        Auth user = Global.GlobalList.IsAuthed(new Guid(part[1]));
                        if (user != null)
                        {
                            switch (part[2])
                            {
                                case "tasks":

                                    return "task";
                                case "mainprofile":

                                    return "profile";
                                case "News":

                                    return "News";
                                case "EmployeeCode":

                                    return "EmpCode";
                                default:
                                    return "shit";
                            }
                        }
                        else return "fuck";
                }
            }
            else return "need more arguments!";
        }

        private string TryAuth(string login, string password)
        {
            using (DataContext db = new DataContext())
            {
                User user = db.Users.Where(User => User.Login == login).FirstOrDefault();
                if(Encoding.UTF8.GetString(user.PassHash) == Encoding.UTF8.GetString(DATA.BLL.Cipher.PassHash(password, user.Salt)))
                {
                    var guid = Guid.NewGuid();
                    Auth auth = new Auth(guid, user);
                    Global.GlobalList.Authorized.Add(auth);
                    string info = string.Empty;
                    var infoEmp = db.Employees.Where(emp => emp.UserID == user.UserID).FirstOrDefault();
                    if (user.PhoneNumber != null)
                    {
                        if (infoEmp != null)
                        {
                            info = user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email + "|" + user.PhoneNumber + "|" + infoEmp.Position + "|" + infoEmp.Company;
                        }
                        else info = user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email + "|" + user.PhoneNumber;
                    }
                    else {
                        if (infoEmp != null)
                        {
                            info = user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email + infoEmp.Position + "|" + infoEmp.Company;
                        } else info = user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email; }
                    return guid.ToString() + "|" + info;
                }
                return "bad";
            }
        }

        private string TryRegistrationOrg(string [] parts)
        {
            using(DataContext db = new DataContext())
            {
                Company company = new Company();
                company.CompanyName = parts[2];
                company.CompanyType = parts[3];
                company.FoundationDate = Convert.ToDateTime(parts[4]);
                company.RegDate = DateTime.Now;
                
            }
        }

        private string TryRegistration(string ABC, string [] parts)
        {
            using (DataContext db = new DataContext())
            {
                User user = new User();
                byte[] salt = DATA.BLL.Cipher.NewSalt();
                byte[] PassHash = DATA.BLL.Cipher.PassHash(parts[3], salt);
                var guid = Guid.NewGuid();
                Auth auth;
                int role = 2;
                string info = string.Empty;
                switch (ABC)
                {
                    case "A":
                        user.Login = parts[2];
                        user.Salt = salt;
                        user.PassHash = PassHash;
                        user.FirstName = parts[4];
                        user.SecondName = parts[5];
                        user.Email = parts[6];
                        user.Birth = Convert.ToDateTime(parts[7]);
                        user.RegDate = DateTime.Today;
                        user.RoleID = role;
                        db.Users.Add(user);
                        db.SaveChanges();
                        user = db.Users.Where(p => p.Login == user.Login).FirstOrDefault();
                        auth = new Auth(guid, user);
                        Global.GlobalList.Authorized.Add(auth);
                        info = user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email;
                        return guid.ToString() + "|" + info;
                    case "Full":
                        user.Login = parts[2];
                        user.Salt = salt;
                        user.PassHash = PassHash;
                        user.FirstName = parts[4];
                        user.SecondName = parts[5];
                        user.Email = parts[6];
                        user.PhoneNumber = parts[7];
                        user.Birth = Convert.ToDateTime(parts[8]);
                        user.RegDate = DateTime.Today;
                        user.RoleID = role;
                        db.Users.Add(user);
                        db.SaveChanges();
                        user = db.Users.Where(p => p.Login == user.Login).FirstOrDefault();
                        auth = new Auth(guid, user);
                        Global.GlobalList.Authorized.Add(auth);
                        info = user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email + "|" + user.PhoneNumber;
                        return guid.ToString() + "|" + info;
                    default: return "meow";
                }
            }
        }
    }
}
