using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using hrm_web_api.Models.Entities;

namespace hrm_web_api.Models.Dtos
{


    public class AddAttendanceDto
    {
        [Required]
        public required Guid EmployeeId { get; set; }

        [Required]
        public required DateTime Date { get; set; }
        [Required]
        public DateTime CheckInTime { get; set; }

        public Status Status { get; set; }
    }
}