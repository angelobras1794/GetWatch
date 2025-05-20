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

        public string? MovieType { get; set; } // 2D, 3D, IMAX, etc.
        
        public string ? CinemaName { get; set; } // Name of the cinema


        
    }
}