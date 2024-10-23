using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrm_web_api.Models.Dtos
{
    public class LoginDto


    {
        public required string Username { get; set; }
        public required  string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

}