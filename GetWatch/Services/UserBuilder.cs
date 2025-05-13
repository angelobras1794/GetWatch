using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces;
using GetWatch.Services.Db;

namespace GetWatch.Services
{
    public class UserBuilder : IUserBuilder
    {
       
        
    private DbUser _user = new DbUser();

    public IUserBuilder SetUserName(string name)
    {
        _user.Name = name;
        return this;
    }

    public IUserBuilder SetUserEmail(string email)
    {
        _user.Email = email;
        return this;
    }

    public IUserBuilder SetUserPassword(string password)
    {
        _user.Password = password;
        return this;
    }

    public IUserBuilder SetUserPhone(string phone)
    {
        _user.Phone = phone;
        return this;
    }

    public DbUser BuildUser()
    {
        return _user;
    }
        


    }
}