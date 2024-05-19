using ClassLibrary_Project_AvanceradNET.Models;
using Microsoft.EntityFrameworkCore;

namespace SUT23_Project_AvanceradNET.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }
        public DbSet<ChangeLog> ChangeLogs { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Test data
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 1,
                CustomerFName = "John",
                CustomerLName = "Persson",
                CustomerPhone = "1234567890",
                CustomerEmail = "j.persson@hotmail.com"
            });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 2,
                CustomerFName = "Fredrik",
                CustomerLName = "Johansson",
                CustomerPhone = "0987654321",
                CustomerEmail = "f.johansson@hotmail.com"
            });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 3,
                CustomerFName = "Sara",
                CustomerLName = "Andersson",
                CustomerPhone = "6543214567",
                CustomerEmail = "s.andersson@hotmail.com"
            });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 4,
                CustomerFName = "Göran",
                CustomerLName = "Svensson",
                CustomerPhone = "3098632789",
                CustomerEmail = "g.svensson@hotmail.com"
            });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 5,
                CustomerFName = "Anna",
                CustomerLName = "Lindblad",
                CustomerPhone = "781234590",
                CustomerEmail = "a.lindblad@hotmail.com"
            });


            modelBuilder.Entity<Company>().HasData(new Company
            {
                CompanyId = 1,
                CompanyName = "Employee1",
                CompanyPassword = "Employee1"
            });

            modelBuilder.Entity<Company>().HasData(new Company
            {
                CompanyId = 2,
                CompanyName = "Employee2",
                CompanyPassword = "Employee2"
            });
        }



        //public void LogChange(DateTime oldDate, DateTime newDate, ProjectDbContext dbContext)
        //{
        //    var logEntry = new ChangeLog
        //    {
        //        EntityName = "Appointment",
        //        PropertyName = "AppointmentDate",
        //        OldDate = oldDate,
        //        NewDate = newDate,
        //        ChangeDate = DateTime.Now
        //    };
        //    dbContext.ChangeLogs.Add(logEntry);
        //    dbContext.SaveChanges();
        //}



    }
}
