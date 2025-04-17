using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using GetWatch.Services.Db;


namespace GetWatch.Interfaces.Db
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> Create<TEntity>() where TEntity: class,IDbItem;
        
    }
}