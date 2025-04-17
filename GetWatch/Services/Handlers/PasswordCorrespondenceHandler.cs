using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;

namespace GetWatch.Services.Handlers
{
    public class PasswordCorrespondenceHandler : UserHandler
    {
        private readonly DbUser _storedUser;

        public PasswordCorrespondenceHandler(DbUser storedUser)
        {
            _storedUser = storedUser;
        }

        public override void Handle(DbUser user)
        {
           

            if (_storedUser.Password != user.Password)
            {
                throw new Exception("Incorrect password.");
            }

            
            NextHandler?.Handle(user);
        }
    }
}