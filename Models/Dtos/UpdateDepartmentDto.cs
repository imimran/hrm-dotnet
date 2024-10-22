using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hrm_web_api.Models.Dtos
{
    public class UpdateDepartmentDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public string? Description { get; set; }

         public Guid? ManagerId { get; set; }
    }
}