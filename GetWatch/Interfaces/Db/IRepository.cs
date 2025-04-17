using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GetWatch.Interfaces.Db
{
    public interface IRepository<TEntity> where TEntity : IDbItem{
        List<TEntity> GetAll();
        TEntity? Get(Guid id);
        void Insert(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity item);

    }
    
}