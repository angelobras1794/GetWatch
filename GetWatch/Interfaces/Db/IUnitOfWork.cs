using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.Db;



namespace GetWatch.Interfaces.Db
{
    public interface IUnitOfWork 
    {
        IRepository<TEntity> ? GetRepository<TEntity>() where TEntity : class, IDbItem;

        void Begin();
        void Commit();
        void Rollback();

        void SaveChanges();
    }
}