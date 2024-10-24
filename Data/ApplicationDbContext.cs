using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hrm_web_api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace hrm_web_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Leave> Leaves { get; set; }

         public DbSet<Payroll> Payrolls { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); // For PostgreSQL

            modelBuilder.Entity<User>()
               .Property(e => e.CreatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP"); // For PostgreSQL

            modelBuilder.Entity<Department>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Attendance>()
               .Property(e => e.CreatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Leave>()
               .Property(e => e.CreatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager) // Define the relationship for the Manager
                .WithMany(e => e.Subordinates) // Each manager can have multiple subordinates
                .HasForeignKey(e => e.ManagerId) // Specify the foreign key
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete to avoid deletion of subordinates when a manager is deleted


            // Configure Employee to Department relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department) // Each employee belongs to one department
                .WithMany(d => d.Employees) // A department can have multiple employees
                .HasForeignKey(e => e.DepartmentId) // Specify the foreign key
                .OnDelete(DeleteBehavior.SetNull); // Set DepartmentId to null if the department is deleted

            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithMany()
                .HasForeignKey(u => u.EmployeeId);


            modelBuilder.Entity<Department>()
                .HasOne(d => d.Employee)
                .WithMany()
                .HasForeignKey(d => d.ManagerId);


            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => a.EmployeeId);

            modelBuilder.Entity<Leave>()
                .HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => a.EmployeeId);

            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.Payrolls)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict); 

        }

    }
}