using HamsterServer.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterServer.DATA
{
    class DataContext :DbContext
    {
        public DataContext() : base("DBConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyLogo> CompanyLogos { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Logo> Logos { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<EmployeeCode> EmployeeCodes { get; set; }
        public DbSet<SystemMessage> SystemMessages { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<TaskExecutors> TaskExecutors { get; set; }
        public DbSet<ProjectsTasks> ProjectsTasks { get; set; }


    }
}
