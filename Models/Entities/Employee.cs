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
        public DateTime CreatedAt { get; private set; }

        public Employee()
        {
            CreatedAt = DateTime.UtcNow; // or DateTime.Now if you prefer local time
        }

    }
}