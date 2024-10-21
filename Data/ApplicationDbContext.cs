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

            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithMany()
                .HasForeignKey(u => u.EmployeeId);


            modelBuilder.Entity<Department>()
                .HasOne(d => d.Employee)
                .WithMany()
                .HasForeignKey(d => d.ManagerId);
        }

    }
}