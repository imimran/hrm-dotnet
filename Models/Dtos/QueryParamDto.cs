using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrm_web_api.Models.Dtos
{
    public class QueryParamDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        
    }

    public class QueryParamWithNameFilter : QueryParamDto
    {

        public string? Name { get; set; } 
  
    }
    public class QueryParamWithUsernameFilter : QueryParamDto
    {
        public string? Username { get; set; } 
  
    }
}