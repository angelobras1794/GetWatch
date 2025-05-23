using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.Db;
using System.Text.RegularExpressions;

namespace GetWatch.Services.Handlers
{
    public class EmailValidationHandler : UserHandler
    {
        public override void Handle(DbUser user)
        {
            Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (string.IsNullOrWhiteSpace(user.Email) || !emailRegex.IsMatch(user.Email))
            {
                throw new Exception("Invalid email format.");
            }

            
            NextHandler?.Handle(user);
        }
    }
}