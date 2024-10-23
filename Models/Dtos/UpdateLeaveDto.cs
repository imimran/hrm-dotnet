using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using hrm_web_api.Models.Entities;

namespace hrm_web_api.Models.Dtos
{
    public class UpdateLeaveDto
    {
        [Required]
        public required Guid EmployeeId { get; set; }
        [Required]
        public required LeaveType LeaveType { get; set; }

        [Required]
        public required DateTime StartDate { get; set; }
        [Required]
        public required DateTime EndDate { get; set; }
        [Required]
        public required LeaveStatus Status { get; set; }

    }
}