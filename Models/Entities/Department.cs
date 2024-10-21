using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrm_web_api.Models.Entities
{
    public class Department
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public  string? Description { get; set; }

        public Guid? ManagerId { get; set; }
        
        public DateTime CreatedAt { get; private set; }

        public Employee? Employee{ get; set; }

        public Department()
        {
            CreatedAt = DateTime.UtcNow;
        }

    }
}