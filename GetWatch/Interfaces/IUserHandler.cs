using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using GetWatch.Services.Db;

namespace GetWatch.Interfaces
{
    public interface IUserHandler
    {
        public void setNext(IUserHandler handler);

        public void handle(DbUser user);


    }
}