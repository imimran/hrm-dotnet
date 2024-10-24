using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrm_web_api.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public required string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public decimal Salary { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? BirthDate { get; set; }

        public Guid? ManagerId { get; set; }
        public Guid? DepartmentId { get; set; }

        public DateTime CreatedAt { get; private set; }

        public Department? Department{ get; private set; }

        // Navigation properties
        public virtual Employee? Manager { get; set; } // Navigation property to refer to the manager
        public virtual ICollection<Employee> Subordinates { get; set; } = new List<Employee>(); // Collection of subordinates


        public Employee()
        {
            CreatedAt = DateTime.UtcNow; // or DateTime.Now if you prefer local time
        }

    }
}