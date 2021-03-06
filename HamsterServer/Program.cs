﻿using HamsterServer.DATA;
using HamsterServer.DATA.Entities;
using HamsterServer.Global;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace HamsterServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DataContext db = new DataContext())
            {
                if (db.Roles.ToList().Count == 0)
                {
                    db.Roles.Add(new Role { RoleName = "Admin" });
                    db.SaveChanges();
                    db.Roles.Add(new Role { RoleName = "User" });
                    db.SaveChanges();
                }
            }
            new Program().ServerListener(new string[] { "http://localhost:8080/" });
        }


        private const string localImagesPath = "./Images/";
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
                HttpListenerResponse response = context.Response;

                Console.WriteLine("Got request from: {0}", request.Url);

                if (request.Url.LocalPath.StartsWith("/getImageFile/")) // Получение изображения из папки
                {
                    byte[] buffer;

                    buffer = File.ReadAllBytes(localImagesPath + request.Url.LocalPath.Substring(14).Replace("/", String.Empty));
                    string responseStr = Convert.ToBase64String(buffer);
                    buffer = System.Text.Encoding.UTF8.GetBytes(responseStr);
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);

                    output.Close();
                }
                else if (request.Url.LocalPath.StartsWith("/uploadImage/")) // загрузка изображения в папку
                {
                    string responseStr;
                    byte[] buffer;
                    try
                    {
                        using (DataContext db = new DataContext())
                        {
                            string[] parts = request.Url.LocalPath.Split('/');
                            string guid = parts[2];
                            User user;
                            User temp = GlobalList.IsAuthed(new Guid(guid)).user;
                            user = db.Users.Where(u => u.UserID == temp.UserID).FirstOrDefault();

                            if (user == null)
                            {
                                throw new Exception("auth err");
                            }

                            string base64 = WebUtility.UrlDecode((new StreamReader(request.InputStream)).ReadToEnd()); // сама картинка

                            byte[] bytes = Convert.FromBase64String(base64.Substring(7)); // превращаем картинку в байты
                            int id;
                         
                            var img = new DATA.Entities.Image
                            {
                                User = user
                            };
                            db.Images.Add(img);
                            db.SaveChanges();
                            //img = db.Images.Last(); //??  :D
                            id = img.ImageID;
                            File.WriteAllBytes(localImagesPath + id.ToString() + ".png", bytes); // записываем байты в папку
                            responseStr = id.ToString();
                        }
                    }
                    catch (Exception Ex)
                    {
                        responseStr = "err";
                    }

                    buffer = System.Text.Encoding.UTF8.GetBytes(responseStr);
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);

                    output.Close();
                }
                else // Если нет работы с картинками используем обычную обработку запросов
                {
                    string responseString = GetResult(WebUtility.UrlDecode(request.Url.LocalPath));

                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);

                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);

                    output.Close();
                }
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
                        if (part.Length != 3) return part.Length.ToString();
                        return GetAvatar(part[2]);
                    case "GetAvatarCompany":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetAvatarCompany(part[2]);
                    case "GetProjects":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetProjects(part[2]);
                    case "GetTasksDone":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetTasksDone(part[2], part[3]);
                    case "GetTasksNotDone":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetTasksNotDone(part[2], part[3]);
                    case "GetTasksDoneCount":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetTasksDoneCount(part[2]);
                    case "GetTasksNotDoneCount":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetTasksNotDoneCount(part[2]);
                    case "GetTasksEndCount":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetTasksEndCount(part[2]);
                    case "GetTasksEnd":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetTasksEnd(part[2], part[3]);
                    case "GetProjects1":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetProjects1(part[2], part[3]);
                    case "GetProject":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetProject(part[2], part[3]);
                    case "GetTask":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetTask(part[2], part[3]);
                    case "CreateProject":
                        if (part.Length != 7) return part.Length.ToString();
                        return CreateProject(part[2], part[3], part[4], part[5], part[6]);
                    case "CreateTask":
                        if (part.Length > 8) return part.Length.ToString();
                        return CreateTask(part[2], part);
                    case "GenerateEmployeeCode":
                        if (part.Length != 3) return part.Length.ToString();
                        return EmpCode(part[2], part[3]);
                    case "SetPosition":
                        if (part.Length != 5) return part.Length.ToString();
                        return SetPosition(part[2], part[3], part[4]);
                    case "SetRights":
                        if (part.Length != 9) return part.Length.ToString();
                        return SetRights(part[2], part[3], part[4], part[5], part[6], part[7], part[8]);
                    case "TaskExecutors":
                        if (part.Length != 5) return part.Length.ToString();
                        return SetExecutor(part[2], part[3], part[4]);
                    case "companyLogoUploaded":
                        if (part.Length != 4) return "err";
                        return companyLogoUploaded(part[2], part[3]);
                    case "avatarUploaded":
                        if (part.Length != 4) return "err";
                        return avatarUploaded(part[2], part[3]);
                    case "MessageCount":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetMesssagesCount(part[2]);
                    case "TaskCount":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetTaskCount(part[2]);
                    case "DeleteTask":
                        if (part.Length != 4) return part.Length.ToString();
                        return RemoveTask(part[2], part[3]);
                    case "GetOneTask":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetOneTask(part[2], part[3]);
                    case "TaskIsDone":
                        if (part.Length != 4) return part.Length.ToString();
                        return TaskIsDone(part[2], part[3]);
                    case "ChooseExec":
                        if (part.Length != 3) return part.Length.ToString();
                        return FindExec(part[2]);
                    case "GetUserProjectsC":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetUserProgectsCount(part[2]);
                    case "GetUserProjects":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetUserProgects(part[2], part[3]);
                    case "GetDepartamentsCount":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetDepartamentsCount(part[2]);
                    case "ChangeLogin":
                        if (part.Length != 4) return part.Length.ToString();
                        return ChangeUserLogin(part[2], part[3]);
                    case "ChangePass":
                        if (part.Length != 4) return part.Length.ToString();
                        return ChangeUserPass(part[2], part[3]);
                    case "ChangeMail":
                        if (part.Length != 4) return part.Length.ToString();
                        return ChangeUserMail(part[2], part[3]);
                    case "LeaveCompany":
                        if (part.Length != 3) return part.Length.ToString();
                        return LeaveCompany(part[2]);
                    case "RemoveProfile":
                        if (part.Length != 3) return part.Length.ToString();
                        return RemoveProfile(part[2]);
                    case "SendMessage":
                        if (part.Length != 6) return part.Length.ToString();
                        return SendMessage(part[2], part[3], part[4], part[5]);
                    case "UserMessagesCheck":
                        if (part.Length != 4) return part.Length.ToString();
                        return UserMessageCheck(part[2], part[3]);
                    case "UserMessagesNotCheck":
                        if (part.Length != 4) return part.Length.ToString();
                        return UserMessageNotCheck(part[2], part[3]);
                    case "UserMessagesCheckCount":
                        if (part.Length != 3) return part.Length.ToString();
                        return CheckMessageCoun(part[2]);
                    case "UserMessagesNotCheckCount":
                        if (part.Length != 3) return part.Length.ToString();
                        return NotCheckMessageCoun(part[2]);
                    case "UserMessage":
                        if (part.Length != 4) return part.Length.ToString();
                        return UserMessage(part[2], part[3]);
                    case "MessageCheck":
                        if (part.Length != 4) return part.Length.ToString();
                        return MessageCheck(part[2], part[3]);
                    case "Inform":
                        if (part.Length != 3) return part.Length.ToString();
                        return Inform(part[2]);
                    case "UpdateTask":
                        if (part.Length != 8) return part.Length.ToString();
                        return UpdateTask(part[2], part[3], part[4], part[5], part[6], part[7]);
                    case "GetProjectTaskCountNotDone":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetProjectTaskCountNotDone(part[2], part[3]);
                    case "GetProjectTaskCountDone":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetProjectTaskCountDone(part[2], part[3]);
                    case "GetProjectTaskCountEnd":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetProjectTaskCountEnd(part[2], part[3]);
                    case "GetProjectTaskDone":
                        if (part.Length != 5) return part.Length.ToString();
                        return GetProjectTaskDone(part[2], part[3], part[4]);
                    case "GetProjectTaskNotDone":
                        if (part.Length != 5) return part.Length.ToString();
                        return GetProjectTaskNotDone(part[2], part[3], part[4]);
                    case "GetProjectTaskEnd":
                        if (part.Length != 5) return part.Length.ToString();
                        return GetProjectTaskEnd(part[2], part[3], part[4]);
                    case "RemoveProject":
                        if (part.Length != 4) return part.Length.ToString();
                        return RemoveProject(part[2], part[3]);
                    case "GetProjectCount":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetProjectCount(part[2]);
                    case "GetProjectnum":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetProjecNum(part[2], part[3]);
                    case "GetDepartment":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetDepartment(part[2], part[3]);
                    case "GetDepartmentInfo":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetDepartmentInfoCount(part[2], part[3]);
                    case "GetDepartmentEmp":
                        if (part.Length != 5) return part.Length.ToString();
                        return GetDepartmentEmp(part[2], part[3], part[4]);
                    case "GetCompanyProjCount":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetCompanyProjCount(part[2]);
                    case "GetCompanyProj":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetCompanyProj(part[2], part[3]);
                    case "GetCompanyEmpsCount":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetCompanyEmpsCount(part[2]);
                    case "GetCompanyEmps":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetCompanyEmps(part[2], part[3]);
                    case "AddDepartament":
                        if (part.Length != 4) return part.Length.ToString();
                        return AddDepartament(part[2], part[3]);
                    case "AddEmpInCompanyCode":
                        if (part.Length != 4) return part.Length.ToString();
                        return AddEmpInCompanyCode(part[2], part[3]);
                    case "EditTask":
                        if (part.Length > 9) return part.Length.ToString();
                        return EditTask(part[2], part);
                    case "GetEditTask":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetEditTask(part[2], part[3]);
                    case "GetEditProject":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetEditProject(part[2], part[3]);
                    case "EditProject":
                        if (part.Length != 8) return part.Length.ToString();
                        return EditProject(part[2], part[3], part[4], part[6], part[7]);
                    case "GetEmp":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetEmp(part[2], part[3]);
                    case "SetEmpPos":
                        if (part.Length != 5) return part.Length.ToString();
                        return SetEmpPos(part[2], part[3], part[4]);
                    case "RemEmp":
                        if (part.Length != 4) return part.Length.ToString();
                        return RemEmp(part[2], part[3]);
                    case "GetDepEmpCount":
                        if (part.Length != 4) return part.Length.ToString();
                        return GetDepEmpCount(part[2], part[3]);
                    case "GetDepEmp":
                        if (part.Length != 5) return part.Length.ToString();
                        return GetDepEmp(part[2], part[3], part[4]);
                    case "EditDep":
                        if (part.Length != 5) return part.Length.ToString();
                        return EditDep(part[2], part[3], part[4]);
                    case "RemDepartment":
                        if (part.Length != 4) return part.Length.ToString();
                        return RemDepartment(part[2], part[3]);
                    case "AddEmpInDepartment":
                        if (part.Length != 5) return part.Length.ToString();
                        return AddEmpInDepartment(part[2], part[3], part[4]);
                    case "Scheldule":
                        if (part.Length != 6) return part.Length.ToString();
                        return Scheldule(part[2], part[3], part[4], part[5]);
                    case "RemoveCompany":
                        if (part.Length != 6) return part.Length.ToString();
                        return RemoveCompany(part[2]);
                    case "GetCompany":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetCompany(part[2]);
                    case "BestEmp":
                        if (part.Length != 3) return part.Length.ToString();
                        return BestEmp(part[2]);
                    case "BestDep":
                        if (part.Length != 3) return part.Length.ToString();
                        return BestDep(part[2]);
                    case "GetAccesRights":
                        if (part.Length != 3) return part.Length.ToString();
                        return GetAccesRights(part[2]);
                    case "RemUser":
                        if (part.Length != 3) return part.Length.ToString();
                        return RemUser(part[2]);
                    case "GetCompanysCount":
                        if (part.Length > 3) return part.Length.ToString();
                        return GetCompanysCount();
                    case "GetCompanys":
                        if (part.Length > 3) return part.Length.ToString();
                        return GetCompanys(part[2]);
                    case "RemCompany":
                        if (part.Length > 3) return part.Length.ToString();
                        return RemCompany(part[2]);
                    case "newAdmin":
                        if (part.Length > 3) return part.Length.ToString();
                        return newAdmin(part[2]);
                    default:
                        return "404";
                }
            }
            else return "need more arguments!";
        }

        private string newAdmin(string mail)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = db.Users.Include("Role").FirstOrDefault(u => u.Email == mail);
                    if(user != null)
                    {
                        Role role = db.Roles.Where(r => r.RoleId == 1).FirstOrDefault();
                        user.Role = role;
                        db.SaveChanges();
                        return "Ok!";
                    } else return "No!";
                }
            }
            catch (Exception x)
            {
                return "No!";
            }
        }

        private string RemCompany(string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    int i = Convert.ToInt32(num);
                    Company company = db.Companies.Where(c => c.CompanyID == i).FirstOrDefault();
                    db.Companies.Remove(company);
                    db.SaveChanges();
                    return "Ok!";
                }
            }
            catch (Exception x)
            {
                return "No!";
            }
        }

        private string GetCompanys(string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    int i = Convert.ToInt32(num);
                    var data = db.Companies.ToList();
                    var t = data[i];
                    var emps = db.Employees.Include("Company").Where(e => e.Company.CompanyID == t.CompanyID).ToList();
                    if (emps.Count() == null && emps.Count() == 0)
                    {
                        return t.CompanyName + "|" + t.CompanyID + "|" + emps.Count();
                    } else return "No!";
                }
            }
            catch (Exception x)
            {
                return "No!";
            }
        }

        private string GetCompanysCount()
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    var data = db.Companies.ToList();
                    return data.Count().ToString();
                }
            }
            catch (Exception x)
            {
                return "No!";
            }
        }

        private string RemUser(string mail)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = db.Users.FirstOrDefault(m => m.Email == mail);
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return "Ok!";
                }
            }
            catch (Exception x)
            {
                return "No!";
            }
        }

        private string GetAccesRights(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        Employee employee = db.Employees.Include("User").FirstOrDefault(e => e.User.UserID == user.UserID);
                        var r = db.AccessRights.Include("Employee").Where(a => a.Employee.EmployeeID == employee.EmployeeID).FirstOrDefault();
                        if(r == null)
                        {
                            return "Ok!";
                        }
                        else
                        {
                            return r.Departament + "|" + r.EmpCode + "|" + r.Projects + "|" + r.Schedule + "|" + r.Tasks;
                        }
                    }
                    else return "No!";
                }
            }
            catch (Exception x)
            {
                return "No!";
            }
        }

        private string BestDep(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {

                        Employee employee = db.Employees.Include("User").Include("Company").FirstOrDefault(e => e.User.UserID == user.UserID);
                        Company company = db.Companies.Include("User").FirstOrDefault(c => c.CompanyID == employee.Company.CompanyID);

                        var depts = db.Departments.Include("DepartamentEmployees").Include("Company").Where(p => p.DepartamentEmployees.Count > 0 && p.Company.CompanyID == company.CompanyID);
                        Dictionary<Department, int> kvp = new Dictionary<Department, int>();
                        foreach (var dept in depts.ToList())
                        {
                            var emps = db.DepartamentEmployees.Include("Employee").Include("User").Where(p => p.Department.DepartamentID == dept.DepartamentID);
                            var users = emps.Select(u => u.Employee);

                            var sum = 0;
                            foreach (var usr in users)
                            {
                                var row = db.TaskExecutors.Include("User").Include("Task");
                                var row1 = row.Where(p => p.User.UserID == usr.User.UserID && p.Task.isDone);
                                var row2 = row1.ToList();
                                var value = row2.Count;
                                sum += value;
                            }
                            kvp.Add(dept, sum);
                        }

                        var ordered = kvp.OrderByDescending(p => p.Value);
                        List<string> l = new List<string>();
                        foreach (var ia in ordered)
                        {
                            l.Add(ia.Key.DepartmentName + "|" + ia.Value.ToString() + "\n");
                        }

                        string s = string.Join("\n", l.ToArray());
                        return s;
                    }
                    else return "No!";
                }
            }
            catch (Exception x)
            {
                return "No!";
            }
        }

        private string BestEmp(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        
                        Employee employee = db.Employees.Include("User").Include("Company").FirstOrDefault(e => e.User.UserID == user.UserID);
                        Company company = db.Companies.Include("User").FirstOrDefault(c => c.CompanyID == employee.Company.CompanyID);

                        var users = db.Employees.Include("User").Include("Company").Where(u => u.Company.CompanyID == company.CompanyID);
                        Dictionary<User, int> kvp = new Dictionary<User, int>();
                        foreach (var usr in users)
                        {
                            var key = usr;
                            var row = db.TaskExecutors.Include("User").Include("Task");
                            var row1 = row.Where(p => p.User.UserID == usr.User.UserID && p.Task.isDone);
                            var row2 = row1.ToList();
                            var value = row2.Count;
                            kvp.Add(key.User, value);
                        }
                        var ordered = kvp.OrderByDescending(p => p.Value);
                        List<string> l = new List<string>();
                        foreach (var ia in ordered) {
                            l.Add(ia.Key.FirstName + " " + ia.Key.SecondName + "|" + ia.Value.ToString() + "\n");
                        }

                        string s = string.Join("\n", l.ToArray());
                        return s;
                    }
                    else return "No!";
                }
            }
            catch (Exception x)
            {
                return "No!";
            }
        }

        private string GetCompany(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        Employee employee = db.Employees.Include("User").Include("Company").FirstOrDefault(e => e.User.UserID == user.UserID);
                        Company company = db.Companies.Include("User").FirstOrDefault(c => c.CompanyID == employee.Company.CompanyID);
                        return company.CompanyName + "|" + company.CompanyType + "|" + company.FoundationDate + "|" + company.User.FirstName + " " + company.User.SecondName;
                    } else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string RemoveCompany(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        Company company = db.Companies.Include("User").FirstOrDefault(c => c.User.UserID == user.UserID);
                        var emps = db.Employees.Include("Company").Include("Employee").Where(e => e.Company.CompanyID == company.CompanyID).ToList();
                        for(int i = 0; i < emps.Count(); i++)
                        {
                            Employee employee = emps[i];
                            db.Employees.Remove(employee);
                            db.SaveChanges();
                        }
                        return "Ok!";
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string Scheldule(string guid, string mail, string start, string end)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        Employee employee = db.Employees.Include("User").FirstOrDefault(e => e.User.Email == mail);
                        DateTime s = Convert.ToDateTime(start);
                        DateTime f = Convert.ToDateTime(end);
                        db.Scheldues.Add(new Sсheldue { Employee = employee, DateStart = s, DateEnd = f });
                        db.SaveChanges();
                        return "Ok!";
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string AddEmpInDepartment(string guid, string num, string mail)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);
                        Department department = db.Departments.FirstOrDefault(d => d.DepartamentID == i);
                        Employee employee = db.Employees.Include("Company").Include("User").FirstOrDefault(c => c.User.Email == mail);
                        DepartamentEmployees departamentEmployees = db.DepartamentEmployees.Include("Employee").FirstOrDefault(l => l.Employee.EmployeeID == employee.EmployeeID);
                        db.DepartamentEmployees.Remove(departamentEmployees);
                        db.SaveChanges();
                        db.DepartamentEmployees.Add(new DepartamentEmployees { Department = department, Employee = employee });
                        db.SaveChanges();
                        return "Ok!";
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string RemDepartment(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);
                        Department department = db.Departments.FirstOrDefault(d => d.DepartamentID == i);
                        var emps = db.DepartamentEmployees.Include("Employee").Include("Depatment").Where(e => e.Department.DepartamentID == department.DepartamentID).ToList();
                        for(int j = 0; j < emps.Count(); j++)
                        {
                            DepartamentEmployees employees = emps[i];
                            db.DepartamentEmployees.Remove(employees);
                            db.SaveChanges();
                        }
                        db.Departments.Remove(department);
                        db.SaveChanges();
                        return "Ok!";
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string EditDep(string guid, string num, string name)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);
                        Department department = db.Departments.FirstOrDefault(d => d.DepartamentID == i);
                        department.DepartmentName = name;
                        db.SaveChanges();
                        return "Ok!";
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetDepEmp(string guid, string num, string d)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);
                        int id = Convert.ToInt32(d);
                        var dep = db.Departments.Include("Company").Where(f => f.DepartamentID == i).FirstOrDefault();
                        var emps = db.DepartamentEmployees.Include("Departament").Include("Employee").Where(e => e.Department.DepartamentID == dep.DepartamentID).ToList();
                        var em = emps[id];
                        var emp = db.Employees.Include("User").Include("Position").FirstOrDefault(j => j.EmployeeID == em.Employee.EmployeeID);
                        return emp.User.FirstName + " " + emp.User.SecondName + "|" + emp.Position + "|" + emp.User.Email + "|" + emp.EmployeeID;

                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetDepEmpCount(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);

                        var dep = db.Departments.Include("Company").Where(d => d.DepartamentID == i).FirstOrDefault();
                        var emps = db.DepartamentEmployees.Include("Departament").Include("Employee").Where(e => e.Department.DepartamentID == dep.DepartamentID).ToList();


                        return emps.Count().ToString();

                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string RemEmp(string guid, string id)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int EmpId = Convert.ToInt32(id);
                        Employee emp = db.Employees.Include("Position").Include("User").FirstOrDefault(e => e.EmployeeID == EmpId);
                        var t = db.TaskExecutors.Include("User").Include("Task").Where(k => k.User.UserID == emp.User.UserID).ToList();
                        for(int i = 0; i < t.Count(); i++)
                        {
                            TaskExecutors ta = t[i];
                            DATA.Entities.Task task = db.Tasks.Include("User").FirstOrDefault(a => a.TaskID == ta.Task.TaskID);
                            User user1 = db.Users.FirstOrDefault(u => u.UserID == task.User.UserID);
                            ta.User = user1;
                            db.SaveChanges();
                        }
                        db.Employees.Remove(emp);
                        return "Ok!";
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string SetEmpPos(string guid, string id, string pos)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int EmpId = Convert.ToInt32(id);
                        Employee emp = db.Employees.Include("Position").Include("User").FirstOrDefault(e => e.EmployeeID == EmpId);
                        Position position = db.Positions.FirstOrDefault(p => p.PositionName == pos);
                        if(position != null)
                        {
                            emp.Position = position;
                            db.SaveChanges();
                        } else
                        {
                            position = db.Positions.Add(new Position {PositionName = pos});
                            db.SaveChanges();
                            emp.Position = position;
                            db.SaveChanges();
                        }
                        return "Ok!";
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetEmp(string guid, string id)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(id);
                        
                        var emp = db.Employees.Include("User").Include("Company").Include("Position").FirstOrDefault(e => e.EmployeeID == i);
                        var dep = db.DepartamentEmployees.Include("Employee").FirstOrDefault(d => d.Employee.EmployeeID == emp.EmployeeID);
                        
                        if (dep != null)
                        {
                            return emp.User.FirstName + " " + emp.User.SecondName + "|" + emp.Position.PositionName + "|" + emp.User.Email + "|" + dep.Department.DepartmentName;
                        } else
                        {
                            return emp.User.FirstName + " " + emp.User.SecondName + "|" + emp.Position.PositionName + "|" + emp.User.Email + "|" + "null";
                        }
                    }
                    else return "No!";
                }
            }
            catch(Exception ex)
            {
                return "No!";
            }
        }

        private string EditProject(string guid, string projectID, string title, string desc, string deadline)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int id = Convert.ToInt32(projectID);
                        User user1 = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
                        Projects projects = db.Projects.Include("User").FirstOrDefault(p => p.ProjectID == id);
                        DateTime date = Convert.ToDateTime(deadline);
                        projects.Title = title;
                        projects.Descript = desc;
                        projects.Deadline = date;
                        return "Ok!";
                    }
                    else return "No!";
                }
            } catch
            {
                return "No!";
            }
        }

        private string GetEditProject(string guid, string projectID)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int id = Convert.ToInt32(projectID);
                        User user1 = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
                        Projects projects = db.Projects.Include("User").FirstOrDefault(p => p.ProjectID == id);
                        return projects.Title + "|" + projects.Descript + "|" + projects.Deadline;

                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetEditTask(string guid, string taskID)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int id = Convert.ToInt32(taskID);
                        User user1 = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
                        DATA.Entities.Task task= db.Tasks.Include("User").FirstOrDefault(t => t.TaskID == id);
                        TaskExecutors taskExecutors = db.TaskExecutors.Include("User").Include("Task").FirstOrDefault(e => e.Task.TaskID == task.TaskID);
                        return task.Title + "|" + task.Descript + "|" + task.Deadline + "|" + task.User.FirstName + " " + task.User.SecondName + "|" + taskExecutors.User.Email; 

                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string EditTask(string guid, string[] part)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User user1 = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
                    TaskExecutors executors = new TaskExecutors();
                    DateTime dead = Convert.ToDateTime(part[7]);
                    switch (part[3])
                    {
                        case "U":
                            int id = Convert.ToInt32(part[4]);
                            DATA.Entities.Task task = db.Tasks.Include("User").Where(u => u.TaskID == id).FirstOrDefault();
                            task.Title = part[5];
                            task.Descript = part[6];
                            DateTime date = Convert.ToDateTime(part[7]);
                            task.Deadline = date;
                            db.SaveChanges();
                            return "Ok!";
                        case "C":
                            int id1 = Convert.ToInt32(part[4]);
                            DATA.Entities.Task task1 = db.Tasks.Include("User").Where(u => u.TaskID == id1).FirstOrDefault();
                            TaskExecutors executors0 = db.TaskExecutors.Include("User").Include("Task").FirstOrDefault(e => e.Task.TaskID == task1.TaskID);
                            task1.Title = part[5];
                            task1.Descript = part[6];
                            DateTime date1 = Convert.ToDateTime(part[7]);
                            task1.Deadline = date1;
                            User user2 = db.Users.FirstOrDefault(u => u.Email == part[8]);
                            executors0.User = user2;
                            db.SaveChanges();
                            return "Ok!";
                        default:
                            return "No!";

                    }
                }
            }catch (Exception ex)
            {
                return "No!";
            }
        }

        private string companyLogoUploaded(string guid, string imageId)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        User user1 = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
                        var emp = db.Employees.Include("User").Include("Company").FirstOrDefault(c => c.User.UserID == user1.UserID);
                        if (emp == null)
                        {
                            throw new Exception();
                        }
                        var comp = db.Companies.FirstOrDefault(c => c.CompanyID == emp.Company.CompanyID);

                        var img = db.Images.FirstOrDefault(i => i.ImageID.ToString() == imageId);
                        var olds = db.CompanyLogos.Include("Company").Where(a => a.Company.CompanyID == comp.CompanyID);
                        foreach (var old in olds)
                        {
                            old.isUsed = false;
                        }

                        db.CompanyLogos.Add(new CompanyLogo
                        {
                            Company = comp,
                            Image = img,
                            isUsed = true
                        });
                        db.SaveChanges();
                        return "Ok!";

                    }
                    else return "No!";
                }
            }
            catch (Exception Ex)
            {
                return "No!";
            }
        }
        private string avatarUploaded(string guid, string imageId)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        User user1 = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
                        
                        var img = db.Images.FirstOrDefault(i => i.ImageID.ToString() == imageId);
                        var olds = db.Avatars.Include("Owner").Where(a => a.Owner.UserID == user1.UserID);
                        foreach (var old in olds)
                        {
                            old.IsUsed = false;
                        }

                        db.Avatars.Add(new Avatar
                        {
                            Owner = user1,
                            Image = img,
                            IsUsed = true
                        });
                        db.SaveChanges();
                        return "Ok!";

                    }
                    else return "No!";
                }
            }
            catch (Exception Ex)
            {
                return "No!";
            }
        }

        private string AddDepartament(string guid, string title)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        User user1 = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
                        Employee employee = db.Employees.Include("Company").Include("User").FirstOrDefault(e => e.User.UserID == user1.UserID);
                        Company company = db.Companies.FirstOrDefault(c => c.CompanyID == employee.Company.CompanyID);
                        db.Departments.Add(new Department { Company = company, DepartmentName = title });
                        db.SaveChanges();
                        return "Ok!";

                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetCompanyEmps(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);
                        var comp = db.Employees.Include("User").Include("Company").Where(c => c.User.UserID == user.UserID).FirstOrDefault();
                        var res = db.Employees.Include("User").Include("Company").Where(r => r.Company.CompanyID == comp.Company.CompanyID).ToList();
                        var emp = res[i];
                        return emp.User.FirstName + " " + emp.User.SecondName + "|" + emp.Position + "|" + emp.User.Email + "|" + emp.EmployeeID;

                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetCompanyEmpsCount(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var comp = db.Employees.Include("User").Include("Company").Where(c => c.User.UserID == user.UserID).FirstOrDefault();
                        var res = db.Employees.Include("Company").Where(r => r.Company.CompanyID == comp.Company.CompanyID).ToList();
                        return res.Count().ToString();

                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetCompanyProj(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);
                        var comp = db.Employees.Include("User").Include("Company").Where(c => c.User.UserID == user.UserID).FirstOrDefault();
                        var proj = db.Projects.Include("User").Include("Company").Where(p => p.Company.CompanyID == comp.Company.CompanyID).ToList();
                        var res = proj[i];
                        return res.Title + "|" + res.Descript + "|" + res.User.FirstName + " " + res.User.SecondName + "|" + res.Deadline + "|" + res.ProjectID.ToString();

                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetCompanyProjCount(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var comp = db.Employees.Include("User").Include("Company").Where(c => c.User.UserID == user.UserID).FirstOrDefault();
                        var proj = db.Projects.Include("User").Include("Company").Where(p => p.Company.CompanyID == comp.Company.CompanyID).ToList();
                        return proj.Count().ToString();

                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetDepartmentEmp(string guid, string num, string id)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int ic = Convert.ToInt32(num);
                        int ie = Convert.ToInt32(id);

                        var data = db.Employees.Include("Company").Include("User").FirstOrDefault(e => e.User.UserID == user.UserID);
                        Company company = db.Companies.FirstOrDefault(c => c.CompanyID == data.Company.CompanyID);
                        var dep = db.Departments.Include("Company").Where(d => d.Company.CompanyID == company.CompanyID).ToList();
                        var r = dep[ic];
                        var emps = db.DepartamentEmployees.Include("Departament").Include("Employee").Where(e => e.Department.DepartamentID == r.DepartamentID).ToList();
                        var emp = emps[ie];
                        var us = db.Employees.Include("User").FirstOrDefault(j => j.EmployeeID == emp.Employee.EmployeeID);
                        return us.User.FirstName + " " + us.User.SecondName + "|" + emp.Employee.Position + "|" + us.User.Email + "|" + us.EmployeeID;

                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetDepartmentInfoCount(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);

                        var data = db.Employees.Include("Company").Include("User").FirstOrDefault(e => e.User.UserID == user.UserID);
                        Company company = db.Companies.FirstOrDefault(c => c.CompanyID == data.Company.CompanyID);
                        var dep = db.Departments.Include("Company").Where(d => d.Company.CompanyID == company.CompanyID).ToList();
                        var r = dep[i];
                        var emps = db.DepartamentEmployees.Include("Departament").Include("Employee").Where(e => e.Department.DepartamentID == r.DepartamentID).ToList();
                        
                        
                        return emps.Count().ToString();

                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetDepartment(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);
                        
                        var data = db.Employees.Include("Company").Include("User").FirstOrDefault(e => e.User.UserID == user.UserID);
                        Company company = db.Companies.FirstOrDefault(c => c.CompanyID == data.Company.CompanyID);
                        var dep = db.Departments.Include("Company").Where(d => d.Company.CompanyID == company.CompanyID).ToList();
                        var r = dep[i];
                        return r.DepartmentName;

                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetProjecNum(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var data = db.Projects.Include("User").Where(d => d.User.UserID == user.UserID).ToList();
                        int i = Convert.ToInt32(num);
                        var p = data[i];
                        var taskcoun = db.ProjectsTasks.Where(t => t.ProjectID == p.ProjectID).Count();
                        var taskcount = db.ProjectsTasks.Include("Task").Where(t => t.ProjectID == p.ProjectID && t.Task.isDone == false).Count();
                        return p.Title + "|" + p.Deadline + "|" + taskcoun.ToString() + "|" + taskcount.ToString() + "|" + p.ProjectID.ToString();
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetProjectCount(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var data = db.Projects.Include("User").Where(d => d.User.UserID == user.UserID).Count();
                        return data.ToString();
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string RemoveProject(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    int id = Convert.ToInt32(num);
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        Projects project = db.Projects.Include("User").Where(p => p.ProjectID == id).FirstOrDefault();
                        var tasks = db.ProjectsTasks.Include("Projects").Include("Task").Where(t => t.ProjectID == id).ToList();
                        if(tasks.Count() != 0)
                        {
                            for(int i = 0; i < tasks.Count(); i++)
                            {
                                DATA.Entities.Task task = tasks[i].Task;
                                var taskex = db.TaskExecutors.Include("User").Include("Task").FirstOrDefault(e => e.Task.TaskID == task.TaskID);
                                db.TaskExecutors.Remove(taskex);
                                db.Tasks.Remove(task);
                                db.SaveChanges();
                            }
                        }
                        db.Projects.Remove(project);
                        db.SaveChanges();
                        return "Ok!";
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetProjectTaskEnd(string guid, string num, string num1)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    int idc = Convert.ToInt32(num);
                    int idt = Convert.ToInt32(num1);
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var data = db.ProjectsTasks.Include("Task").Include("Projects").Where(t => t.ProjectID == idc && t.Task.isDone == false && t.Task.Deadline > DateTime.Today).ToList();
                        var d = data[idt];
                        var ex = db.TaskExecutors.Include("User").Include("Task").FirstOrDefault(e => e.Task.TaskID == d.Task.TaskID);
                        return d.Task.Title + "|" + ex.User.FirstName + " " + ex.User.SecondName + "|" + d.Task.Deadline + "|" + d.Task.User.FirstName + " " + d.Task.User.SecondName + "|" + d.Task.TaskID;
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetProjectTaskNotDone(string guid, string num, string num1)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    int idc = Convert.ToInt32(num);
                    int idt = Convert.ToInt32(num1);
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var data = db.ProjectsTasks.Include("Task").Include("Projects").Where(p => p.ProjectID == idc && p.Task.isDone == false && p.Task.Deadline > DateTime.Today).ToList();
                        var d = data[idt];
                        var ex = db.TaskExecutors.Include("User").Include("Task").FirstOrDefault(e => e.Task.TaskID == d.Task.TaskID);
                        return d.Task.Title + "|" + ex.User.FirstName + " " + ex.User.SecondName + "|" + d.Task.Deadline + "|" + d.Task.User.FirstName + " " + d.Task.User.SecondName + "|" + d.Task.TaskID;
                    }
                    else return "No!";
                }
            }
            catch(Exception ex)
            {
                return "No!";
            }
        }

        private string GetProjectTaskCountEnd(string guid, string num)
        {

            try
            {
                using (DataContext db = new DataContext())
                {
                    int id = Convert.ToInt32(num);
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var data = db.ProjectsTasks.Include("Task").Include("Projects").Where(d => d.ProjectID == id && d.Task.isDone == false && d.Task.Deadline < DateTime.Today).Count();
                        return data.ToString();
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetProjectTaskCountDone(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    int id = Convert.ToInt32(num);
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var data = db.ProjectsTasks.Include("Task").Include("Projects").Where(d => d.ProjectID == id && d.Task.isDone == true).Count();
                        return data.ToString();
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetProjectTaskDone(string guid, string num, string num1)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    int idc = Convert.ToInt32(num);
                    int idt = Convert.ToInt32(num1);
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var data = db.ProjectsTasks.Include("Task").Include("Projects").Where(t => t.ProjectID == idc && t.Task.isDone == true).ToList();
                        var d = data[idt];
                        var ex = db.TaskExecutors.Include("User").Include("Task").FirstOrDefault(e => e.Task.TaskID == d.Task.TaskID);
                        return d.Task.Title + "|" + ex.User.FirstName + " " + ex.User.SecondName + "|" + d.Task.Deadline + "|" + d.Task.User.FirstName + " " + d.Task.User.SecondName + "|" + d.Task.TaskID;
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetProjectTaskCountNotDone(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    int id = Convert.ToInt32(num);
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var data = db.ProjectsTasks.Include("Task").Include("Projects").Where(d => d.ProjectID == id && d.Task.isDone == false && d.Task.Deadline > DateTime.Today).Count();
                        return data.ToString();
                    } else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string UpdateTask(string guid, string num, string title, string desk, string deadline, string exec)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    int id = Convert.ToInt32(exec);
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User ex = db.Users.FirstOrDefault(u => u.UserID == id);
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);
                        DATA.Entities.Task task = db.Tasks.Include("User").Where(t => t.TaskID == i).FirstOrDefault();
                        TaskExecutors taskExecutors = db.TaskExecutors.Include("User").Include("Task").FirstOrDefault(e => e.Task.TaskID == task.TaskID);
                        if(task.User.UserID == user.UserID)
                        {
                            task.Title = title;
                            task.Descript = desk;
                            task.Deadline = Convert.ToDateTime(deadline);
                            taskExecutors.User = ex;
                            db.SaveChanges();
                            return "Ok!";
                        }
                        else return "No!";
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetTasksEnd(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);
                        var data = db.TaskExecutors.Include("User").Include("Task").Where(t => t.User.UserID == user.UserID && t.Task.Deadline <= DateTime.Today && t.Task.isDone == false).ToList();
                        DATA.Entities.Task task = db.Tasks.Include("User").FirstOrDefault(m);
                        bool m(DATA.Entities.Task t)
                        {
                            return t.TaskID == data[i].Task.TaskID;
                        }
                        return task.Title + "|" + data[i].User.FirstName + " " + data[i].User.SecondName + "|" + task.Deadline + "|" + task.User.FirstName + " " + task.User.SecondName + "|" + task.TaskID;
                    }
                    else return "No!";
                }
            }
            catch (Exception Ex)
            {
                return "No!";
            }
        }

        private string GetTasksNotDone(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);
                        var data = db.TaskExecutors.Include("User").Include("Task").Where(t => t.User.UserID == user.UserID && t.Task.isDone == false && t.Task.Deadline > DateTime.Today).ToList();
                        var d = data[i];
                        DATA.Entities.Task task = db.Tasks.Include("User").FirstOrDefault(t => t.TaskID == d.Task.TaskID);
                        return task.Title + "|" + data[i].User.FirstName + " " + data[i].User.SecondName + "|" + task.Deadline + "|" + task.User.FirstName + " " + task.User.SecondName + "|" + task.TaskID;
                    }
                    else return "No!";
                }
            }
            catch (Exception Ex)
            {
                return "No!";
            }
        }


        private string GetTasksDone(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int i = Convert.ToInt32(num);
                        var data = db.TaskExecutors.Include("User").Include("Task").Where(t => t.User.UserID == user.UserID && t.Task.isDone == true).ToList();
                        var d = data[i];
                        DATA.Entities.Task task = db.Tasks.Include("User").FirstOrDefault(t => t.TaskID == d.Task.TaskID);
                        return task.Title + "|" + data[i].User.FirstName + " " + data[i].User.SecondName + "|" + task.Deadline + "|" + task.User.FirstName + " " + task.User.SecondName + "|" + task.TaskID;
                    }
                    else return "No!";
                }
            }
            catch (Exception Ex)
            {
                return "No!";
            }
        }


        private string GetTasksEndCount(string guid)
        {
            try
            {
                
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var data = db.TaskExecutors.Include("User").Include("Task").Where(t => t.User.UserID == user.UserID && t.Task.Deadline <= DateTime.Today && t.Task.isDone == false).Count();
                        return data.ToString();
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetTasksDoneCount(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var data = db.TaskExecutors.Include("User").Include("Task").Where(t => t.User.UserID == user.UserID && t.Task.isDone == true).Count();
                        return data.ToString();
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetTasksNotDoneCount(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        var data = db.TaskExecutors.Include("User").Include("Task").Where(t => t.User.UserID == user.UserID && t.Task.isDone == false && t.Task.Deadline > DateTime.Today).Count();
                        return data.ToString();
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string Inform(string guid)
        {
            try
            {
                string info = string.Empty;
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
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
                    if (user != null)
                    {
                        if (user.PhoneNumber != null)
                        {
                            if (infoEmp != null)
                            {
                                info = user.UserID + "|" + user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email + "|" + user.PhoneNumber + "|" + positionName.PositionName + "|" + companyName.CompanyName + "|" + "WP";
                            }
                            else info = user.UserID + "|" + user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email + "|" + user.PhoneNumber;
                        }
                        else
                        {
                            if (infoEmp != null)
                            {
                                info = user.UserID + "|" + user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email + positionName.PositionName + "|" + companyName.CompanyName + "|" + "W";
                            }
                            else info = user.UserID + "|" + user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email;
                        }
                        return guid.ToString() + "|" + info;
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string MessageCheck(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int id = Convert.ToInt32(num);
                        Message mes = db.Messages.FirstOrDefault(m => m.MessageID == id);
                        mes.isCheck = true;
                        db.SaveChanges();
                        return "Ok!";
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string UserMessage(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user != null)
                    {
                        int id = Convert.ToInt32(num);
                        Message mess = db.Messages.Include("UserTo_UserId").Include("UserFrom_UserId").FirstOrDefault(m => m.MessageID == id);
                        return mess.Title + "|" + mess.Content + "|" + mess.TimeSend + "|" + mess.UserFrom_UserId.FirstName + " " + mess.UserFrom_UserId.SecondName + "|" + mess.UserFrom_UserId.Email ;
                    } else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string UserMessageNotCheck(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User UpUser = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                    int i = Convert.ToInt32(num);
                    var data1 = db.Messages.Include("UserTo_UserId").Include("UserFrom_UserId");
                    var data2 = data1.Where(m => m.UserTo_UserId.UserID == UpUser.UserID && m.isCheck == false);
                    var data = data2.ToList();
                    return data[i].MessageID + "|" + data[i].Title + "|" + data[i].TimeSend + "|" + data[i].UserFrom_UserId.FirstName + " " + data[i].UserFrom_UserId.SecondName;
                }
            }
            catch (Exception Ex)
            {
                return "No!";
            }
        }

        private string NotCheckMessageCoun(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User UpUser = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                    var mesg = db.Messages;
                    var withUser = mesg.Include("User");
                    var whereIs = withUser.Where(m => m.UserTo_UserId.UserID == UpUser.UserID && m.isCheck == false);
                    var data = whereIs.Count();
                    if(data !=0) return data.ToString();
                    else return "No!";
                }
            }
            catch (Exception Ex)
            {
                return "No!";
            }
        }

        private string CheckMessageCoun(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User UpUser = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                    var data = db.Messages.Include("User").Where(m => m.UserTo_UserId.UserID == UpUser.UserID && m.isCheck == true).Count();
                    if (data != 0) return data.ToString();
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string UserMessageCheck(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User UpUser = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                    int i = Convert.ToInt32(num);
                    var data = db.Messages.Include("UserTo_UserId").Include("UserFrom_UserId").Where(m => m.UserTo_UserId.UserID == UpUser.UserID && m.isCheck == true).ToList();
                    return data[i].MessageID + "|" + data[i].Title + "|" + data[i].TimeSend + "|" + data[i].UserFrom_UserId.FirstName + " " + data[i].UserFrom_UserId.SecondName;
                    
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string SendMessage(string guid, string to, string title, string content)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User UpUser = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                    User UserTo = db.Users.FirstOrDefault(t => t.Email == to);
                    if (UserTo != null)
                    {
                        DateTime sendtime = DateTime.Now;
                        db.Messages.Add(new Message { Title = title, Content = content, isCheck = false, TimeSend = sendtime, UserFrom_UserId = UpUser, UserTo_UserId = UserTo });
                        db.SaveChanges();
                        return "Ok!";
                    } else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string RemoveProfile(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User UpUser = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                    db.Users.Remove(UpUser);
                    db.SaveChanges();
                    return "Ok!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string LeaveCompany(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User UpUser = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                    Employee emp = db.Employees.Include("User").FirstOrDefault(e => e.User.UserID == UpUser.UserID);
                    db.Employees.Remove(emp);
                    db.SaveChanges();
                    return "Ok!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string ChangeUserMail(string guid, string mail)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User UpUser = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                    UpUser.Email = mail;
                    db.SaveChanges();
                    return "Ok!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string ChangeUserPass(string guid, string pass)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User UpUser = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                    byte[] passHash = DATA.BLL.Cipher.PassHash(pass, user.Salt);
                    UpUser.PassHash = passHash;
                    db.SaveChanges();
                    return "Ok!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string ChangeUserLogin(string guid, string login)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User UpUser = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                    UpUser.Login = login;
                    db.SaveChanges();
                    return "Ok!";
                }
            }
            catch {
                return "No!";
            }
        }

        private string GetDepartamentsCount(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    var data = db.Employees.Include("Company").Include("User").FirstOrDefault(e => e.User.UserID == user.UserID);
                    Company company = db.Companies.FirstOrDefault(c => c.CompanyID == data.Company.CompanyID);
                    var dep = db.Departments.Include("Company").Where(d => d.Company.CompanyID == company.CompanyID).Count();
                    if (dep != null)
                    {
                        return dep.ToString();
                    }
                    else return "No!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetUserProgects(string guid, string num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    int i = Convert.ToInt32(num);
                    var data = db.Projects.Include("User").Where(p => p.User.UserID == user.UserID).ToList();
                    Projects project = db.Projects.Include("User").FirstOrDefault(p => p.User.UserID == user.UserID && data[i].ProjectID == p.ProjectID);
                    string name = project.Title;
                    string descript = project.Descript;
                    string dead = project.Deadline.ToString();
                    string projectID = project.ProjectID.ToString();

                    var tasks = db.ProjectsTasks.Include("Task").Where(t => t.ProjectID == project.ProjectID);
                    int countask = tasks.Count();
                    var done = db.ProjectsTasks.Include("Task").Where(t => t.ProjectID == project.ProjectID && t.Task.isDone == true);
                    int countdone = done.Count();
                    string dones = countask.ToString() + "/" + countdone.ToString();
                    return projectID + "|" + name + "|" + descript + "|" + dead + "|" + dones;
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetUserProgectsCount(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    var data = db.Projects.Include("User").Where(p => p.User.UserID == user.UserID).ToList();
                    if (data == null)
                    {
                        return "Null";
                    }
                    else return data.Count.ToString();
                }
            }
            catch
            {
                return "No!";
            }
        }
        private string FindExec(string guid)
        {
            try{
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    var data = db.Employees.Include("Company").Include("User").FirstOrDefault(d => d.User.UserID == user.UserID);
                    var emp = db.Employees.Include("Company").Include("User").Where(d => d.Company.CompanyID == data.Company.CompanyID).ToList();
                    string res = string.Empty;
                    for (int i = 0; i < emp.Count; i++)
                    {
                        res += emp[i].User.UserID + "/" + emp[i].User.FirstName + " " + emp[i].User.SecondName + "|";
                    }
                    return res;
                }
            } catch
            {
                return "Something wrong";
            }
        }

        private string TaskIsDone(string guid, string TaskID)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    int id = Convert.ToInt32(TaskID);
                    DATA.Entities.Task task = db.Tasks.Include("User").FirstOrDefault(t => t.TaskID == id);
                    TaskExecutors executors = db.TaskExecutors.Include("Task").Include("User").FirstOrDefault(e => e.Task.TaskID == id);

                    if (task.User.UserID == user.UserID || executors.User.UserID == user.UserID)
                    {
                        task.isDone = true;
                        db.SaveChanges();
                        return "IsDone";
                    }
                    else return "You can't!";
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string RemoveTask(string guid, string TaskID)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    int id = Convert.ToInt32(TaskID);
                    DATA.Entities.Task data = db.Tasks.Include("User").FirstOrDefault(t => t.TaskID == id);
                    if (data.User.UserID == user.UserID)
                    {
                        db.Tasks.Remove(data);
                        db.SaveChanges();
                        return "Deleted!";
                    }
                    else return "You cant delete someone else's task!";
                }
            }catch
            {
                return "No!";
            }

        }

        private string GetTaskCount(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    int count = db.TaskExecutors.Include("User").Where(u => u.User.UserID == user.UserID && u.Task.Deadline > DateTime.Now && u.Task.isDone == false).Count();

                    if (count > 0)
                    {
                        return count.ToString();
                    }
                    else return "No tasks";
                }
            } catch
            {
                return "No!";
            }
        }

        private string GetMesssagesCount(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;

                    int count = db.Messages.Include("UserTo_UserId").Where(m => m.UserTo_UserId.UserID == user.UserID && m.isCheck == false).Count();

                    if (count > 0)
                    {
                        return count.ToString();
                    }
                    else return "No messages";
                }
            }
            catch
            {
                return "No!";
            }
        }


        private string SetExecutor(string guid, string TaskID, string UserID)
        {
            try
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
            catch
            {
                return "No!";
            }
        }

        private string SetRights(string guid, string mail,  string Task, string Departament, string Projects, string EmpCode, string Schedule)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    Employee employee = db.Employees.Include("User").FirstOrDefault(e => e.User.Email == mail);
                    bool T = Convert.ToBoolean(Task);
                    bool D = Convert.ToBoolean(Departament);
                    bool P = Convert.ToBoolean(Projects);
                    bool E = Convert.ToBoolean(EmpCode);
                    bool S = Convert.ToBoolean(Schedule);
                    db.AccessRights.Add(new AccessRight { Tasks = T, Departament = D, Projects = P, EmpCode = E, Schedule = S, Employee = employee });
                    db.SaveChanges();
                    return "Ok!";
                }
            } catch
            {
                return "No!";
            }
        }

        private string SetPosition(string guid, string mail, string pos)
        {
            try
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
                    return "Ok!";
                }
            } catch
            {
                return "No!";
            }
        }

        private string AddEmpInCompanyCode(string guid, string code)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User temp = db.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                    int compID = db.Employees.Include("Company").Include("User").FirstOrDefault(c => c.User.UserID == temp.UserID).Company.CompanyID;
                    Company company = db.Companies.FirstOrDefault(c => c.CompanyID == compID);
                    var req = db.EmployeeCodes.FirstOrDefault(c => c.Code == code);
                    if (req != null)
                    {
                        Employee employee = new Employee();
                        employee.User = temp;
                        employee.Company = company;
                        EmployeeCode employeeCode = req;
                        employeeCode.isUsed = true;
                        db.SaveChanges();
                        return "Set a position";
                    }
                    else return "Try again!";

                }
            } catch
            {
                return "No!";
            }
        }

        private string EmpCode(string guid, string email)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    if (user.UserID != null)
                    {
                        User user1 = db.Users.FirstOrDefault(u => user.UserID == u.UserID);
                        User UserTo = db.Users.FirstOrDefault(t => t.Email == email);
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
                        string mes = "You have a company code!" + " " + res;
                        db.Messages.Add(new Message { Content = mes, Title = "Company Code", isCheck = false, TimeSend = DateTime.Now, UserFrom_UserId = user, UserTo_UserId = UserTo });
                        db.SaveChanges();
                        return "Code is created" + "|" + res;
                    }
                    else return "No!";
                }
            }
            catch { 
                return "No!";
            }
        }

        private string CreateTask(string guid, string[] part)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User user1 = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
                    TaskExecutors executors = new TaskExecutors();
                    DateTime dead = Convert.ToDateTime(part[6]);
                    switch (part[3])
                    {
                        case "U":
                            DATA.Entities.Task temp = db.Tasks.Add(new DATA.Entities.Task { User = user1, Deadline = dead, CreateDate = DateTime.Now, Descript = part[5], isDone = false, Title = part[4] });
                            db.TaskExecutors.Add(new TaskExecutors { User = user1, Task = temp });
                            db.SaveChanges();
                            return "Task is added";
                        case "C":
                            DATA.Entities.Task temp1 = db.Tasks.Add(new DATA.Entities.Task { User = user1, Deadline = dead, CreateDate = DateTime.Now, Descript = part[5], isDone = false });

                            User exec = db.Users.FirstOrDefault(u => u.Email == part[7]);
                            db.TaskExecutors.Add(new TaskExecutors { Task = temp1, User = exec });
                            db.SaveChanges();
                            return "Task is added";
                        case "UP":
                            DATA.Entities.Task temp2 = db.Tasks.Add(new DATA.Entities.Task { User = user1, Deadline = dead, CreateDate = DateTime.Now, Descript = part[5], isDone = false, Title = part[4] });
                            int i = Convert.ToInt32(part[7]);
                            Projects projects = db.Projects.Include("User").FirstOrDefault(p => p.ProjectID == i);
                            db.ProjectsTasks.Add(new ProjectsTasks { Projects = projects, Task = temp2 });
                            db.TaskExecutors.Add(new TaskExecutors { User = user1, Task = temp2 });
                            db.SaveChanges();
                            return "Task is added";
                        case "CP":
                            DATA.Entities.Task temp3 = db.Tasks.Add(new DATA.Entities.Task { User = user1, Deadline = dead, CreateDate = DateTime.Now, Descript = part[5], isDone = false });
                            int j = Convert.ToInt32(part[part.Length - 1]);
                            Projects projects1 = db.Projects.Include("User").FirstOrDefault(p => p.ProjectID == j);
                            User exec1 = db.Users.FirstOrDefault(u => u.Email == part[7]);
                            db.ProjectsTasks.Add(new ProjectsTasks { Projects = projects1, Task = temp3 });
                            db.TaskExecutors.Add(new TaskExecutors { Task = temp3, User = exec1 });
                            db.SaveChanges();
                            return "Task is added";
                        default:
                            return "no";

                    }
                }
            }
            catch
            {
                return "no";
            }
             
        }

        private string CreateProject(string guid, string A, string title, string descript, string deadline)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    User user1 = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
                    Projects projects = new Projects();
                    switch (A)
                    {
                        case "U":
                            projects.Title = title;
                            projects.Descript = descript;
                            projects.User = user1;
                            projects.Deadline = Convert.ToDateTime(deadline);
                            db.Projects.Add(projects);
                            db.SaveChanges();
                            return "Ok!";
                        case "C":
                            projects.Title = title;
                            projects.Descript = descript;
                            projects.User = user1;
                            projects.Deadline = Convert.ToDateTime(deadline);
                            Employee infoEmp = db.Employees.Include("User").Include("Position").Include("Company").FirstOrDefault(emp => emp.User.UserID == user.UserID);
                            Company company = db.Companies.FirstOrDefault(c => c.CompanyID == infoEmp.Company.CompanyID);
                            projects.Company = company;
                            db.Projects.Add(projects);
                            db.SaveChanges();
                            return "Ok!";
                        default:
                            return "No!";
                    }
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetProjects(string guid)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    var data = db.Projects.Include("User").Where(p => p.User.UserID == user.UserID).ToList();
                    return data.Count.ToString();
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetProjects1(string guid, string Num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    int i = Convert.ToInt32(Num);
                    var data = db.Projects.Include("User").Where(p => p.User.UserID == user.UserID).ToList();
                    var d = data[i];
                    string title = d.Title;
                    string content = d.Descript;
                    string creater = d.User.FirstName + " " + d.User.SecondName;
                    return title + "|" + content + "|" + creater + "|" + d.Deadline.ToString() + "|" + d.ProjectID.ToString();
                }
            }
            catch
            {
                return "No!";
            }
        }

        private string GetProject(string guid, string Num)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    User user = GlobalList.IsAuthed(new Guid(guid)).user;
                    int i = Convert.ToInt32(Num);
                    var data = db.Projects.Include("User").Where(p => p.ProjectID == i).FirstOrDefault();
                    
                    string title = data.Title;
                    string content = data.Descript;
                    string creater = data.User.FirstName + " " + data.User.SecondName;
                    return title + "|" + content + "|" + creater + "|" + data.Deadline.ToString() + "|" + data.ProjectID.ToString();
                }
            }
            catch
            {
                return "No!";
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

        private string GetOneTask(string guid, string Id)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                int i = Convert.ToInt32(Id);
                var task = db.Tasks.Include("User").Where(t => t.TaskID == i).FirstOrDefault();
                var execU = db.TaskExecutors.Include("Task").Include("User").FirstOrDefault(e => e.Task.TaskID == task.TaskID);
                string title = task.Title;
                string Descript = task.Descript;
                string deadline = task.Deadline.ToString();
                string creater = task.User.FirstName + " " + task.User.SecondName;
                string exec = execU.User.FirstName + " " + execU.User.SecondName;
                string done = task.isDone.ToString();
                string TaskID = Convert.ToString(task.TaskID);
                return TaskID + "|" + title + "|" + Descript + "|" + deadline + "|" + creater + "|" + exec + "|" + done;
            }
        }

        private string GetTask(string guid, string Num)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                int i = Convert.ToInt32(Num);
                var task = db.Tasks.Include("User").Where(t => t.TaskID == i).FirstOrDefault();
                var data = db.TaskExecutors.Include("User").Include("Task").FirstOrDefault(d => d.Task.TaskID == task.TaskID);
                string title = task.Title;
                string Descript = task.Descript;
                string deadline = task.Deadline.ToString();
                string creater = task.User.FirstName + " " + task.User.SecondName;
                string exec = data.User.FirstName + " " + data.User.SecondName;
                string done = task.isDone.ToString();
                string TaskID = Convert.ToString(task.TaskID);
                return TaskID + "|" + title + "|" + Descript + "|" + deadline + "|" + creater + "|" + exec + "|" + done;
            }
        }

        private string GetAvatar(string guid)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                var data = db.Avatars.Include("Owner").Include("Image").FirstOrDefault(a => a.Owner.UserID == user.UserID && a.IsUsed == true);
                if (data == null)
                {
                    return "No Avatar";
                }
                else
                {
                    string str = "no";
                    try
                    {
                        byte[] buffer = File.ReadAllBytes(localImagesPath + "/" + data.ImageID + ".png");
                        str = Convert.ToBase64String(buffer);
                    }
                    catch { }
                    return str;
                }
            }
        }


        private string GetAvatarCompany(string guid)
        {
            using (DataContext db = new DataContext())
            {
                User user = GlobalList.IsAuthed(new Guid(guid)).user;
                var emp = db.Employees.Include("User").Include("Company").FirstOrDefault(c => c.User.UserID == user.UserID);
                if (emp == null)
                {
                    throw new Exception();
                }
                var comp = db.Companies.FirstOrDefault(c => c.CompanyID == emp.Company.CompanyID);

                var logo = db.CompanyLogos.Include("Company").Include("Image").FirstOrDefault(a => a.Company.CompanyID == comp.CompanyID);
                if (logo == null)
                {
                    return "No Avatar";
                }
                else
                {
                    string str = "no";
                    try
                    {
                        byte[] buffer = File.ReadAllBytes(localImagesPath + "/" + logo.Image.ImageID + ".png");
                        str = Convert.ToBase64String(buffer);
                    }
                    catch { }
                    return str;
                }
            }
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
                            company.User = db.Users.Where(p => p.UserID == id).FirstOrDefault();
                        }
                        else return "No!";
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
                        Directory.CreateDirectory(@"\Documents\Companies\" + company.CompanyName);
                        Directory.CreateDirectory(@"\Documents\Companies\" + company.CompanyName + @"\Files\Images");
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
                        Directory.CreateDirectory(@"Documents\Users\" + user.UserID.ToString());
                        Directory.CreateDirectory(@"Documents\Users\" + user.UserID.ToString() + @"\Images");
                        info = user.UserID + "|" + user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email;
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
                        user.RegDate = DateTime.Now;
                        user.RoleID = role;
                        db.Users.Add(user);
                        db.SaveChanges();
                        user = db.Users.Where(p => p.Login == user.Login).FirstOrDefault();
                        auth = new Auth(guid, user);
                        Global.GlobalList.Authorized.Add(auth);
                        Directory.CreateDirectory(@"Documents\Users\" + user.UserID.ToString());
                        Directory.CreateDirectory(@"Documents\Users\" + user.UserID.ToString() + @"\Images");
                        info = user.UserID + "|" + user.RoleID + "|" + user.FirstName + "|" + user.SecondName + "|" + user.Birth + "|" + user.Email + "|" + user.PhoneNumber;
                        return guid.ToString() + "|" + info;
                    default: return "meow";
                }
            }
        }
    }
}
