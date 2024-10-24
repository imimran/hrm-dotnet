using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using hrm_web_api.Models.Entities;

namespace hrm_web_api.Models.Dtos
{
    public class AddPayrollDto
    {
        [Required]
        public required Guid EmployeeId { get; set; }

        public decimal Amount { get; set; } // Amount of the payroll 
        public decimal Deductions { get; set; } // Deductions of the payroll 
        public decimal Tax { get; set; } // Tax of the payroll 
        public DateTime PaidDate { get; set; } // Date of the payroll
        public Methods PaymentMethod { get; set; } // Date of the payroll
    }
}