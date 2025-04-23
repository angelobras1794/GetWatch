using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetWatch.Services.Movies
{
    public class GenreWithMovies
    {

     public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<PopularApiMovie> Movies { get; set; } = new List<PopularApiMovie>();   
    }
}