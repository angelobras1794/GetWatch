using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces;
using GetWatch.Interfaces.Db;

namespace GetWatch.Services.Db
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly GetWatchContext dbContext;
        public RepositoryFactory(GetWatchContext context)
        {
            dbContext = context;
        }
        public IRepository<TEntity> Create<TEntity>()
            where TEntity : class, IDbItem
        {
            return new Repository<TEntity>(dbContext); 
        }
        
    }
}