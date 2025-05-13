using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.Db;

namespace GetWatch.Interfaces
{
    public interface IUserBuilder
    {
        
        public IUserBuilder SetUserName(string name);
        public IUserBuilder SetUserPassword(string password);
        public IUserBuilder SetUserEmail(string email);
        public IUserBuilder SetUserPhone(string phone);
        
        public DbUser BuildUser();

    }
}