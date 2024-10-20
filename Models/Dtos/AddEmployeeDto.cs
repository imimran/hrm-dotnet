using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hrm_web_api.Models.Dtos
{
    public class AddEmployeeDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}