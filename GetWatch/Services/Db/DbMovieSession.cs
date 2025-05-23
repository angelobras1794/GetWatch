using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetWatch.Services.Db
{
    public class DbMovieSession : DbItem
    {
        public int MovieId { get; set; }

        public string[]? AvailableSeats { get; set; }

        public string? MovieType { get; set; } 
        
        public string ? CinemaName { get; set; } 


        
    }
}