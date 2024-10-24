using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrm_web_api.Models.Entities
{

    public enum Methods
    {
        Cash,
        Bank,
        MFS
    }

    public class Payroll
    {
        public Guid Id { get; set; }
        public required Guid EmployeeId { get; set; }

        public decimal Amount { get; set; } // Amount of the payroll 
        public decimal Deductions { get; set; } // Deductions of the payroll 
        public decimal Tax { get; set; } // Tax of the payroll 
        public DateTime PaidDate { get; set; } // Date of the payroll
        public Methods PaymentMethod { get; set; } // Date of the payroll

        // Navigation property to reference Employee
        public virtual Employee Employee { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public Payroll()
        {
            this.CreatedAt = CreatedAt;
        }
    }


}