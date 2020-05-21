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
                    case "RegOrgU":
                        if (part.Length != 9) return part.Length.ToString();
                        return TryRegistrationOrg("A", part);
                    case "GetAvatarUser":
                        if(part.Length != 3) return part.Length.ToString();
                        return GetAvatar(part[2]);
                    case "GetProjects":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetAvatar(part[2]);
                    case "GetTasks":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetAvatar(part[2]);
                    case "CreateProject":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetAvatar(part[2]);
                    case "CreateTask":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetAvatar(part[2]);
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

        private string GetAvatar(string UserID)
        {
            return "wow";
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
                    Employee infoEmp = db.Employees.Include("User").Include("Position").Include("Company").FirstOrDefault(emp => emp.User.UserID == user.UserID);
                    Position positionName = null;
                    Company companyName = null;
                    if (infoEmp != null)
                    {
                        int pos = infoEmp.Position.PositionID;
                        int comp = infoEmp.Company.CompanyID;
                        positionName = db.Positions.Where(p => p.PositionID == pos).FirstOrDefault();
                        companyName = db.Companies.Where(p => p.CompanyID == comp).FirstOrDefault();
                    }
                    if (user.PhoneNumber != null)
                    {
                        if (infoEmp != null)
                        {
                            info = user.UserID + "|" + user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email + "|" + user.PhoneNumber + "|" + positionName.PositionName + "|" + companyName.CompanyName + "|" + "WP";
                        }
                        else info = user.UserID + "|" + user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email + "|" + user.PhoneNumber;
                    }
                    else {
                        if (infoEmp != null)
                        {
                            info = user.UserID + "|" + user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email + positionName.PositionName + "|" + companyName.CompanyName + "|" + "W";
                        } else info = user.UserID + "|" + user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email; }
                    return guid.ToString() + "|" + info;
                }
                return "bad";
            }
        }

        private string TryRegistrationOrg(string AB, string [] parts)
        {
            using (DataContext db = new DataContext())
            {
                Company company = new Company();
                Employee employee = new Employee();
                Position position = new Position();
                switch (AB) {
                    case "A":
                        company.CompanyName = parts[2];
                        company.CompanyType = parts[3];
                        company.FoundationDate = Convert.ToDateTime(parts[4]);
                        company.RegDate = DateTime.Now;
                        string[] infoUser = TryAuth(parts[5], parts[6]).Split('|');
                        if (infoUser != null)
                        {
                            int id = Convert.ToInt32(infoUser[1]);
                            employee.User = db.Users.Where(p => p.UserID == id).FirstOrDefault();
                            company.UserID = id;
                        }
                        else return "Логин или пароль введены неверно или такого пользователя не сущетствует";
                        db.Companies.Add(company);
                        db.SaveChanges();
                        
                        employee.Company = company;
                        string name = parts[7];
                        position = db.Positions.Where(p => p.PositionName == name).FirstOrDefault();

                        if (position != null)
                        {
                            employee.Position = position;
                        } else {
                            position = new Position();
                            position.PositionName = parts[7];
                            db.Positions.Add(position);
                            db.SaveChanges();
                            employee.Position = position;
                        }

                        db.Employees.Add(employee);
                        db.SaveChanges();
                        return TryAuth(parts[5], parts[6]);
                    default: return "WrongKeys";
            }

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
