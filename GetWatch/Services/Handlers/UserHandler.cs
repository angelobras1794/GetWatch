using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.Db;

namespace GetWatch.Services.Handlers
{
    public abstract class UserHandler
    {
        protected UserHandler? NextHandler;

        public void SetNext(UserHandler nextHandler)
        {
            NextHandler = nextHandler;
        }

        public abstract void Handle(DbUser user);
    }
}