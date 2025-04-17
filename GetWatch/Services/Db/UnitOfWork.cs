using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;
using Microsoft.EntityFrameworkCore.Storage;

namespace GetWatch.Services.Db
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction? Transaction { get; set; }
        private IRepositoryFactory RepositoryFactory { get; set; }
        private GetWatchContext Context { get; set; }
        private IDictionary<Type, object> Repositories { get; } = new Dictionary<Type,object>();

        public UnitOfWork(GetWatchContext context, IRepositoryFactory repositoryFactory)
        {
            Context = context;
            RepositoryFactory = repositoryFactory;
        }
        public void Begin()
        {
            Transaction = Context.Database.BeginTransaction();
        }
        public void Commit()
        {
            Transaction?.Commit();
            Transaction = null;
        }
        public void Rollback()
        {
            Transaction?.Rollback();
            Transaction = null;
        }
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        IRepository<TEntity> IUnitOfWork.GetRepository<TEntity>()
        {
            if (!Repositories.ContainsKey(typeof(TEntity)))
            {
                Repositories[typeof(TEntity)] = RepositoryFactory.Create<TEntity>();
            }

            return (IRepository<TEntity>)Repositories[typeof(TEntity)];
        }
        
    }
}