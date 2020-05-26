using HamsterServer.DATA;
using HamsterServer.DATA.Entities;
using HamsterServer.Global;

using System;
using System.Collections.Generic;
using System.IO;
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


                
                HttpListenerResponse response = context.Response;
                
                string responseString = GetResult(request.Url.LocalPath);
                
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);

                using (DataContext db = new DataContext())
                {
                    if(db.Roles.ToList().Count == 0)
                    {
                        db.Roles.Add(new Role { RoleName = "Admin" });
                        db.SaveChanges();
                        db.Roles.Add(new Role { RoleName = "User" });
                        db.SaveChanges();
                    }
                }
                
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
                        return GetProjects(part[2]);
                    case "GetTasks":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetTasks(part[2]);
                    case "GetProject":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetProject(part[2], part[3]);
                    case "GetTask":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetTask(part[2], part[3]);
                    case "CreateProject":
                        if (part.Length != 7) return part.Length.ToString();
                        return CreateProject(part[2], part[3], part[4], part[5]);
                    case "CreateTask":
                        if (part.Length != 8) return part.Length.ToString();
                        return CreateTask(part[2], part[3], part[4], part[5], part[6], part[7]);
                    case "GenerateEmployeeCode":
                        if (part.Length != 3) return part.Length.ToString();
                        return EmpCode(part[2]);
                    case "AddMeInCompany":
                        if (part.Length != 5) return part.Length.ToString();
                        return AddEmpInCompany(part[2], part[3], part[4]);
                    case "SetPosition":
                        if (part.Length != 4) return part.Length.ToString();
                        return SetPosition(part[2], part[3]);
                    case "SetRights":
                        if (part.Length != 11) return part.Length.ToString();
                        return SetRights(part[2], part[3], part[4], part[5], part[6], part[7], part[8], part[9], part[10]);
                    case "TaskExecutors":
                        if (part.Length != 5) return part.Length.ToString();
                        return SetExecutor(part[2], part[3], part[4]);
                    case "UploadImage":
                        if (part.Length != 5) return part.Length.ToString();
                        return UpUmage(part[2], part[4]);
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

        private string SetExecutor(string guid, string TaskID, string UserID)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                int ExecID = Convert.ToInt32(UserID);
                int temp = Convert.ToInt32(TaskID);
                User Exec = db.Users.FirstOrDefault(u => u.UserID == ExecID);
                DATA.Entities.Task T = db.Tasks.FirstOrDefault(t => t.TaskID == temp);
                db.TaskExecutors.Add(new TaskExecutors { Task = T, User = Exec });
                db.SaveChanges();
                return "Ok";
            }
        }

        private string SetRights(string guid, string UserID, string Report, string Task, string Departament, string Projects, string EmpCode, string Schedule, string Marks)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                int ID = Convert.ToInt32(UserID);
                Employee employee = db.Employees.Include("User").FirstOrDefault(e => e.User.UserID == ID);
                bool R = Convert.ToBoolean(Report);
                bool T = Convert.ToBoolean(Task);
                bool D = Convert.ToBoolean(Departament);
                bool P = Convert.ToBoolean(Projects);
                bool E = Convert.ToBoolean(EmpCode);
                bool S = Convert.ToBoolean(Schedule);
                bool M = Convert.ToBoolean(Marks);
                db.AccessRights.Add(new AccessRight { Report = R, Tasks = T, Departament = D, Projects = P, EmpCode = E, Schedule = S, Marks = M });
                db.SaveChanges();
                return "Access rights saved";
            }
        }

        private string SetPosition(string guid, string pos)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                Employee emp = db.Employees.FirstOrDefault(e => e.User.UserID == user.UserID);
                Position position = db.Positions.Where(p => p.PositionName == pos).FirstOrDefault();

                if (position != null)
                {
                    emp.Position = position;
                }
                else
                {
                    position = new Position();
                    position.PositionName = pos;
                    db.Positions.Add(position);
                    db.SaveChanges();
                    emp.Position = position;
                }
                db.SaveChanges();
                return "It's ok";
            }
        }

        private string AddEmpInCompany(string guid, string companyID, string code)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                int compID = Convert.ToInt32(companyID);
                Company company = db.Companies.FirstOrDefault(c => c.CompanyID == compID);
                var req = db.EmployeeCodes.FirstOrDefault(c => c.Code == code);
                if(req != null)
                {
                    Employee employee = new Employee();
                    employee.User = user;
                    employee.Company = company;
                    EmployeeCode employeeCode = req;
                    employeeCode.isUsed = true;
                    db.SaveChanges();
                }
                return "Set a position";
            }
        }

        private string EmpCode(string guid)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                var temp = db.Employees.Include("User").FirstOrDefault(u => u.User.UserID == user.UserID);
                Company company = db.Companies.FirstOrDefault(c => c.CompanyID == temp.Company.CompanyID);
                StringBuilder builder = new StringBuilder();
                Random rnd = new Random();
                char a;
                for (int i = 0; i < 6; i++)
                {
                    a = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rnd.NextDouble() + 65)));
                    builder.Append(a);
                }
                string res = builder.ToString();
                EmployeeCode code = new EmployeeCode();
                code.Code = res;
                code.Company = company;
                code.isUsed = false;
                db.EmployeeCodes.Add(code);
                db.SaveChanges();
                return "Code is created";
            }
        }

        private string CreateTask(string guid, string A, string title, string descript, string deadline, string executor)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                DATA.Entities.Task task = new DATA.Entities.Task();
                TaskExecutors executors = new TaskExecutors();
                
                switch (A)
                {
                    case "U":
                        task.Title = title;
                        task.Descript = descript;
                        task.Deadline = Convert.ToDateTime(deadline);
                        task.User = user;
                        db.Tasks.Add(task);
                        db.SaveChanges();
                        executors.Task = task;
                        executors.User = user;
                        db.TaskExecutors.Add(executors);
                        db.SaveChanges();
                        return "Task is added";
                    case "E":
                        task.Title = title;
                        task.Descript = descript;
                        task.Deadline = Convert.ToDateTime(deadline);
                        task.User = user;
                        db.Tasks.Add(task);
                        db.SaveChanges();
                        int id = Convert.ToInt32(executor);
                        User exec = db.Users.FirstOrDefault(u => u.UserID == id);
                        executors.Task = task;
                        executors.User = exec;
                        db.TaskExecutors.Add(executors);
                        db.SaveChanges();
                        return "Task is added";
                    default:
                        return "NO SWEETHEART";

                }
            }
        }

        private string CreateProject(string guid, string A, string title, string descript)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                Projects projects = new Projects();
                switch (A)
                {
                    case "U":
                        projects.Title = title;
                        projects.Descript = descript;
                        projects.User = user;
                        db.Projects.Add(projects);
                        db.SaveChanges();
                        return "It's done!";
                    case "C":
                        projects.Title = title;
                        projects.Descript = descript;
                        projects.User = user;
                        int CompID = db.Employees.Include("Company").Include("User").FirstOrDefault(e => e.User.UserID == user.UserID).Company.CompanyID;
                        Company company = db.Companies.FirstOrDefault(c => c.CompanyID == CompID);
                        projects.Company = company;
                        db.Projects.Add(projects);
                        db.SaveChanges();
                        return "It's done!";
                    default:
                        return "Something wrong!";
                }
            }
        }

        private string GetProjects(string ID)
        {
            using (DataContext db = new DataContext())
            {
                int id = Convert.ToInt32(ID);
                var data = db.Projects.Include("User").Where(p => p.User.UserID == id).ToList();
                return data.Count.ToString();
            }
        }

        private string GetProject(string ID,string Num)
        {
            using (DataContext db = new DataContext())
            {
                int id = Convert.ToInt32(ID);
                int i = Convert.ToInt32(Num);
                var data = db.Projects.Include("User").Where(p => p.User.UserID == id).ToList();
                string title = data[i].Title;
                string content = data[i].Descript;
                string creater = data[i].User.FirstName + " " + data[i].User.SecondName;
                return title + "|" +  content + "|" + creater;
            }
        }


        private string GetTasks(string guid)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                var data = db.TaskExecutors.Include("User").Where(p => p.User.UserID == user.UserID).ToList();
                return data.Count.ToString();
            }
        }

        private string GetTask(string guid, string Num)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                int i = Convert.ToInt32(Num);
                var data = db.TaskExecutors.Include("User").Include("Task").Where(p => p.User.UserID == user.UserID).ToList();
                var d = data[i];
                var task = db.Tasks.Include("User").Where(t => t.TaskID == d.Task.TaskID).FirstOrDefault();
                string title = task.Title;
                string Descript = task.Descript;
                string deadline = task.Deadline.ToString();
                string creater = task.User.FirstName + " " + task.User.SecondName;
                string exec = data[i].User.FirstName + " " + task.User.SecondName;
                string TaskID = Convert.ToString(task.TaskID);
                return TaskID + "|" + title + "|" + Descript + "|" + deadline + "|" + creater + "|" + exec;
            }
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
                        Directory.CreateDirectory(@"\Documents\" + company.CompanyName);
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
