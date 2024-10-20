using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrm_web_api.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }

        public Guid? EmployeeId { get; set; }

        public required string Password { get; set; }

        public required string Role { get; set; }

        public Employee? Employee { get; set; }

        public DateTime CreatedAt { get; private set; }

        public User()
        {
            CreatedAt = DateTime.UtcNow; // or DateTime.Now if you prefer local time
        }


    }
}