using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.Db;


namespace GetWatch.Services.Db
{
    public class DbUser : DbItem
    {
        public string Name { get; set; }
        public string Password { get; set; } 
        public string Email { get; set; } 
        public string Phone { get; set; }  = string.Empty;
        public bool IsAdmin { get; set; } = false;
        
       
        
    }
}