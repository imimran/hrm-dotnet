using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hrm_web_api.Models.Dtos
{
    public class AddUserDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }


        [Required]
        public required string Role { get; set; }
        
    }
}