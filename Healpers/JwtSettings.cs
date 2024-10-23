using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrm_web_api.Healpers
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty; // JWT Secret key
        public string Issuer { get; set; } = string.Empty;    // Issuer of the token
        public string Audience { get; set; } = string.Empty;  // Audience for the token
        public int ExpiryMinutes { get; set; }  
    }
}