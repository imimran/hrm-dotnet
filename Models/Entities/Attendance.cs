using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrm_web_api.Models.Entities
{
    public enum Status
    {
        Present,
        Absent,
        Leave
    }
    public class Attendance
    {


        public Guid Id { get; set; }
        public required Guid EmployeeId { get; set; }

        public required DateTime Date { get; set; }
        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public Status Status { get; set; } = Status.Absent;

        public DateTime CreatedAt { get; private set; }

        public Employee? Employee{ get; set; }

        public Attendance()
        {
            this.CreatedAt = DateTime.UtcNow;
        }

    }
}