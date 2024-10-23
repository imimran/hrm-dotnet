using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrm_web_api.Models.Entities
{

    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected,
        Cancelled
    }

    public enum LeaveType
    {
        Sick,
        Casual,
        Maternity
    }


    public class Leave
    {
        public Guid Id { get; set; }
        public required Guid EmployeeId { get; set; }

        public required LeaveType LeaveType { get; set; }
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }

        public Employee? Employee { get; set; }

        public DateTime CreatedAt { get; set; }

        public Leave()
        {
            this.CreatedAt = CreatedAt;
        }

    }
}