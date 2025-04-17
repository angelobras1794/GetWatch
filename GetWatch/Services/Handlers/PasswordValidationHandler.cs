using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.Db;
using System.Text.RegularExpressions;

namespace GetWatch.Services.Handlers
{
    public class PasswordValidationHandler : UserHandler
    {
        public override void Handle(DbUser user)
        {
           Regex passRegex= new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (string.IsNullOrWhiteSpace(user.Password) || !passRegex.IsMatch(user.Password))
            {
                throw new Exception("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
            }
            

            // Pass to the next handler
            NextHandler?.Handle(user);
        }
    }
}