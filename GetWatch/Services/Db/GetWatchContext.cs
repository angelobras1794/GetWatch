using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GetWatch.Services.Db
{
    public class GetWatchContext : DbContext
    {
        public DbSet<DbUser> DbUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=GetWatch.db"); 
            

        }

        
    }
}
