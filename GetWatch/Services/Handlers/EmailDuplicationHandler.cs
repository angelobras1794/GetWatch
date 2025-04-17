using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.Db;

namespace GetWatch.Services.Handlers
{
    public class EmailDuplicationHandler : UserHandler
    {
        private readonly List<string> _existingEmails;

        public EmailDuplicationHandler(List<string> existingEmails)
        {
            _existingEmails = existingEmails;
        }

        public override void Handle(DbUser user)
        {
            if (_existingEmails.Contains(user.Email))
            {
                throw new Exception("Email is already registered.");
            }

            // Pass to the next handler
            NextHandler?.Handle(user);
        }
    }
}