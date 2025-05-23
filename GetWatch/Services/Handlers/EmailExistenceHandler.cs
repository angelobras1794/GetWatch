using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;

namespace GetWatch.Services.Handlers
{
    public class EmailExistenceHandler : UserHandler
    {
        private readonly List<string> _existingEmails;

        public EmailExistenceHandler(List<string> existingEmails)
        {
            _existingEmails = existingEmails;
        }

        public override void Handle(DbUser user)
        {
            if (!_existingEmails.Contains(user.Email))
            {
                throw new Exception("Email does not exist.");
            }

            
            NextHandler?.Handle(user);
        }
    }
}