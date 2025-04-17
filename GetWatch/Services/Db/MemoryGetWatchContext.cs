using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  GetWatch.Services.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;


namespace GetWatch.Services.Db
{
    public class MemoryGetWatchContext : GetWatchContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            SqliteConnection connection = new("DataSource=:memory:");
            connection.Open();

            options.UseSqlite(connection);
        }
    }
   
}