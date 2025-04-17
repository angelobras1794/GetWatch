using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;
using Microsoft.EntityFrameworkCore;

namespace GetWatch.Services.Db
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IDbItem
    {
        public GetWatchContext Context { get; set; }

        public Repository(GetWatchContext context)
        {
            Context = context;  
        }

        public List<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public TEntity? Get(Guid id)
        {
            return Context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public void Insert(TEntity item)
        {
            Context.Set<TEntity>().Add(item);
        }

        public void Update(TEntity item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(TEntity item)
        {
            Context.Remove(item);
        }
    }
}