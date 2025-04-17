using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.Db;

namespace GetWatch.Services.Handlers
{
    public class UsernameDuplicationHandler : UserHandler
    {
        private readonly List<string> _existingUsernames;

        public UsernameDuplicationHandler(List<string> existingUsernames)
        {
            _existingUsernames = existingUsernames;
        }



        public override void Handle(DbUser user)
        {
            if (_existingUsernames.Contains(user.Name))
            {
                throw new Exception("Username is already taken.");
            }

            // Pass to the next handler
            NextHandler?.Handle(user);
        }
    }
}